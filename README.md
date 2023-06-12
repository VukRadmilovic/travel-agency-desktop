# Uputstvo za pokretanje

Ovo uputstvo će vam pokazati kako da pokrenete ovu WPF aplikaciju koja je rađena u Visual Studio 2022 i koristi Entity Framework. Molimo Vas da pratite korake ispod da biste uspešno pokrenuli projekat.

## Prethodne pripreme
Pre nego što krenete, uverite se da imate instalirane sledeće komponente:

1. **Visual Studio 2022**: Preuzmite i instalirajte Visual Studio 2022 sa zvanične Microsoftove veb lokacije.
2. **Entity Framework**: Ako nemate instaliran Entity Framework, možete ga instalirati pomoću Package Manager Console-a u Visual Studio-u. Otvorite Package Manager Console iz menija **Tools > NuGet Package Manager > Package Manager Console** i unesite sledeću komandu: `Install-Package EntityFramework`

## Pokretanje projekta

1. Klonirajte ili preuzmite izvorni kod projekta sa GitHub-a.
2. Otvorite Visual Studio 2022.
3. Izaberite opciju **Open a project or solution** sa početnog ekrana ili idite na **File > Open > Project/Solution**.
4. Pronađite i otvorite folder u koji ste preuzeli ili klonirali izvorni kod projekta.

## Konfiguracija lokalne baze podataka

Da biste pravilno koristili aplikaciju, morate izvršiti inicijalnu migraciju kako biste kreirali bazu podataka i popunili je početnim podacima. Sledite korake ispod:

1. Otvorite **Package Manager Console** iz menija **Tools > NuGet Package Manager > Package Manager Console**.
2. Unutar te novootvorene konzole na dnu ekrana ukucajte `Add-Migration migracija` i pritisnite enter
3. Kada je migracija kreirana, kucajte `Update-Database` i pritisnite enter

### Napomene 
- Proverite da li je SQL Server pokrenut preko SQL Server object explorer-a i da li imate odgovarajući Connection String u konfiguracionom fajlu projekta.
- Da biste se ulogovali morate imati korisnika koji može da se uloguje. Korisnika dodajte preko SQL Server Object Explorera, direktno unoseći podatke u user tabelu. Do podataka ćete doći desnim klikom na user tabelu pa levim klikom na "View Data".
- Ukoliko budete imali poteškoća sa kreiranjem migracije, probajte da obrišete sve prethodne migracije iz foldera migrations kao i sve tabele iz SQL servera, pa onda kreirate novu migraciju

## Pokretanje aplikacije

Sada kada je projekat konfigurisan i baza podataka je spremna, možete pokrenuti aplikaciju. Sledite korake ispod:

1. Proverite da li je WPF projekat označen kao **StartUp Project**. Desnim klikom na projekat u Solution Explorer-u, izaberite **Set as StartUp Project**.
2. Izaberite odgovarajuću konfiguraciju iz **Solution Configurations** padajućeg menija. Na primer, **Debug**.
3. Kliknite na **Start** dugme ili pritisnite F5 da biste pokrenuli aplikaciju.

Aplikacija će se pokrenuti i trebali biste biti u mogućnosti da je koristite.

## Napomene

- Ako se pojave greške tokom migracije ili prilikom pokretanja aplikacije, proverite da li su sve potrebne zavisnosti instalirane i ispravno konfigurisane.
- Ako ne možete da pronađete bazu podataka nakon migracije, proverite Connection String u konfiguracionom fajlu i uverite se da je ispravno podešen.
- Ukoliko imate dodatna pitanja ili problema, molimo vas da posetite zvaničnu dokumentaciju Entity Framework-a i Visual Studio-a za više informacija.
