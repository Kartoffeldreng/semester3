
using Microsoft.AspNetCore.Mvc;
using PersonApp.Models;
using PersonApp.Repository;
using System.Diagnostics;

namespace PersonApp.Controllers {

    // HomeController styrer, hvad der sker, når brugeren besøger hjemmesiden.
    // Den håndterer både visning af personer, registrering og download af billeder.
    public class HomeController : Controller {

        // Felt, der giver adgang til PersonRepository.
        // Det bruges til at hente og gemme personer.
        private readonly IPersonRepository _personRepository;

        // Felt, der giver adgang til FileRepository.
        // Det bruges til at gemme og hente billedfiler.
        private readonly IFileRepository _fileRepository;

        // Constructor:
        // Bliver automatisk kaldt, når HomeController oprettes.
        // ASP.NET Core sender de to repositories ind via Dependency Injection.
        public HomeController(IPersonRepository personRepository, IFileRepository fileRepository) {

            // Gemmer det modtagne PersonRepository i feltet.
            _personRepository = personRepository;

            // Gemmer det modtagne FileRepository i feltet.
            _fileRepository = fileRepository;
        }

        // GET-metode:
        // Bliver kaldt, når brugeren åbner hjemmesiden.
        public IActionResult Index() {

            // Henter alle registrerede personer fra PersonRepository.
            // IEnumerable<Person> betyder, at vi får en samling af Person-objekter.
            IEnumerable<Person> persons = _personRepository.GetAll();

            // Sender listen med personer videre til Index.cshtml.
            // Viewet kan nu vise alle registrerede personer.
            return View(persons);
        }

        // POST-metode:
        // Bliver kaldt, når brugeren indsender registreringsformularen.
        [HttpPost]
        public IActionResult Index(string name, IFormFile file) {

            // Henter alle eksisterende personer.
            // Dette bruges, hvis der opstår en fejl, så listen stadig vises.
            IEnumerable<Person> persons = _personRepository.GetAll();

            // Kontrollerer, om brugeren har indtastet et navn.
            // IsNullOrWhiteSpace kontrollerer også for tomme mellemrum.
            if (string.IsNullOrWhiteSpace(name)) {

                // Sender en fejlbesked til Viewet.
                ViewData["Message"] = "Indtast navn, før du registrerer.";

                // Viser siden igen med den eksisterende personliste.
                return View(persons);
            }

            // Kontrollerer, om brugeren har valgt en fil.
            // file.Length == 0 betyder, at filen ikke indeholder data.
            if (file is null || file.Length == 0) {

                // Sender en fejlbesked til Viewet.
                ViewData["Message"] = "Vælg en billedfil, før du registrerer.";

                // Viser siden igen.
                return View(persons);
            }

            // Kontrollerer, om den uploadede fil rent faktisk er et billede.
            if (!IsImage(file)) {

                // Hvis filen ikke er et godkendt billede, vises en fejl.
                ViewData["Message"] = "Kun billedfiler (jpg, png, gif) er tilladt.";

                return View(persons);
            }

            // Åbner billedets indhold som en Stream.
            // using sørger for, at streamen lukkes automatisk bagefter.
            using (Stream stream = file.OpenReadStream())

                // Sender filnavnet og billedets indhold til FileRepository,
                // som gemmer billedet på computeren.
                _fileRepository.Save(file.FileName, stream);

            // Opretter et nyt Person-objekt.
            Person person = new Person {

                // Gemmer personens navn.
                Name = name,

                // Gemmer billedets filnavn på personen.
                // Selve billedet ligger allerede i filsystemet.
                ImageFileName = file.FileName
            };

            // Tilføjer den nye person til PersonRepository.
            _personRepository.Add(person);

            // Kalder Save() for at gemme ændringen i repository.
            _personRepository.Save();

            // TempData gemmer en besked, som kan vises efter en redirect.
            // Beskeden forsvinder normalt, efter den er blevet læst.
            TempData["Message"] = $"Personen '{person.Name}' blev registreret.";

            // Sender brugeren tilbage til Index-siden.
            // Det betyder, at GET Index() bliver kaldt igen.
            return RedirectToAction(nameof(Index));
        }

        // Download-metode:
        // Bruges, når brugeren klikker på download-knappen ved et billede.
        public IActionResult Download(string fileName) {

            // Forsøger at åbne billedet gennem FileRepository.
            // Hvis billedet ikke findes, returneres null.
            Stream? stream = _fileRepository.Open(fileName);

            // Kontrollerer, om billedet eksisterer.
            if (stream is null)

                // Returnerer HTTP 404, hvis filen ikke findes.
                return NotFound();

            // Sender filen tilbage til browseren som en download.
            // GetContentType finder filens MIME-type.
            // fileName bruges som navnet på den downloadede fil.
            return File(stream, GetContentType(fileName), fileName);
        }

        // Hjælpemetode, der bestemmer filens MIME-type ud fra filtypen.
        // MIME-typen fortæller browseren, hvilken slags fil der sendes.
        private static string GetContentType(string fileName)

            // Finder filens filtypenavn og konverterer det til små bogstaver.
            // switch-expression vælger den rigtige MIME-type.
            => Path.GetExtension(fileName).ToLowerInvariant() switch {

                // JPG og JPEG er billedfiler.
                ".jpg" or ".jpeg" => "image/jpeg",

                // PNG er en billedfil.
                ".png" => "image/png",

                // GIF er en billedfil.
                ".gif" => "image/gif",

                // Hvis filtypen ikke genkendes, bruges en generel filtype.
                _ => "application/octet-stream",
            };

        // Indeholder de første bytes, som kendetegner de tilladte billedformater.
        // Disse kaldes filsignaturer eller magic numbers.
        private static readonly byte[][] _imageSignatures = [

            [0xFF, 0xD8, 0xFF],       // jpg
            [0x89, 0x50, 0x4E, 0x47], // png
            [0x47, 0x49, 0x46, 0x38], // gif
        ];

        // Kontrollerer, om den uploadede fil rent faktisk er et billede.
        private static bool IsImage(IFormFile file) {

            // Åbner filen, så vi kan læse dens indhold.
            using Stream stream = file.OpenReadStream();

            // Opretter en byte-array med plads til 4 bytes.
            // Vi bruger disse bytes til at undersøge filens signatur.
            byte[] header = new byte[4];

            // Læser de første 4 bytes fra filen.
            // read fortæller, hvor mange bytes der faktisk blev læst.
            int read = stream.Read(header, 0, header.Length);

            // Gennemløber alle de tilladte billedsignaturer.
            foreach (byte[] signature in _imageSignatures)

                // Kontrollerer, om filens første bytes matcher en signatur.
                // SequenceEqual sammenligner byte-værdierne.
                if (signature.Length <= read &&
                    header.Take(signature.Length).SequenceEqual(signature))

                    // Hvis en signatur passer, er filen godkendt.
                    return true;

            // Hvis ingen signatur passer, er filen ikke et godkendt billede.
            return false;

        }
    }
}