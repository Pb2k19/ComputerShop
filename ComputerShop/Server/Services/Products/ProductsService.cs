using ComputerShop.Server.DataAccess;
using ComputerShop.Server.Helpers;
using ComputerShop.Shared.Models;
using ComputerShop.Shared.Models.Components;
using ComputerShop.Shared.Models.Products;
using MongoDB.Driver;

namespace ComputerShop.Server.Services.Products
{
    public class ProductsService : IProductsService
    {
        private ProductHelper productHelper = new();
        private IProductData productsData;

        public ProductsService(IProductData productsData)
        {
            this.productsData = productsData;
        }

        public List<Product> Products = new()
        {
            new CableProduct
            {
                CabelType = "HDMI 2.1",
                Color = "Czarny",
                ConnectorA = "HDMI 2.1",
                ConnectorB = "HDMI 2.1",
                Price = 70,
                PriceBeforeDiscount = 70,
                Manufacturer = "Unitek",
                Lenghtmm = 1500,
                Name = "Kabel HDMI 2.1 - HDMI 1,5m (8K/60Hz, 4K/120Hz)",
                WarantyMonths = 24,
                Description = "Przewód HDMI marki Unitek wykonano z najwyższej jakości materiałów, aby zapewnić kompatybilność z technologią HDMI 2.1, oferującą przepustowość na poziomie nawet 48 Gb/s. Zapewnia ona krystalicznie czysty i wyraźny obraz wyświetlany w rozdzielczościach 8K/60Hz oraz 4K/120Hz. Ponadto wspiera funkcje Dynamic HDR, Dolby Vision, HDR 10 czy 3D Video.",
                Images = new(){ new Image{Location = "https://cdn.x-kom.pl/i/setup/images/prod/big/product-new-big,,2019/8/pr_2019_8_19_12_17_50_317_01.jpg"} },
                IsPublic = true,
                Comments = new(){new Comment { Name = "Andrzej", Score = 3, Text = "Dobre, pomarańczowe"} }
            },
            new CableProduct
            {
                CabelType = "USB-C",
                Color = "Czarny",
                ConnectorA = "USB-C",
                ConnectorB = "USB-C",
                Price = 69,
                PriceBeforeDiscount = 89,
                Manufacturer = "Unitek",
                Lenghtmm = 1000,
                Name = "Kabel USB-C - USB-C - PD 100W, 10 Gbps",
                WarantyMonths = 24,
                Description = "Kabel Unitek USB-C - USB-C ze standardem 3.1 Gen 2 umożliwia przesyłanie danych z prędkością 10 Gbps. Pozwoli Ci to na sprawną synchronizację plików, a kopiowanie dysku czy zdjęć przebiega z najszybszą możliwą prędkością.",
                Images = new(){ new Image{Location = "https://cdn.x-kom.pl/i/setup/images/prod/big/product-new-big,,2021/8/pr_2021_8_19_9_15_32_451_02.jpg"} },
                IsPublic = true,
            },
            new CableProduct
            {
                CabelType = "HDMI",
                Color = "Czarny",
                ConnectorA = "HDMI",
                ConnectorB = "HDMI",
                Price = 20,
                PriceBeforeDiscount = 20,
                Manufacturer = "Unitek",
                Lenghtmm = 1500,
                Name = "Kabel Jack 3.5mm - Jack 3.5mm 1,5m",
                WarantyMonths = 24,
                Description = "Unitek Y-C922ABK to kabel przekazujący sygnał audio, który umożliwia podłączenie urządzenia wyposażonego w gniazdo minijack 3,5mm do innego urządzenia posiadającego również gniazdo minijack 3,5mm. Dzięki temu możliwe jest podłączenie np. komputera lub smartfona do wieży Hi-Fi.",
                Images = new(){ new Image{Location = "https://cdn.x-kom.pl/i/setup/images/prod/big/product-new-big,,pr_2016_9_14_14_37_38_962.jpg"} },
                IsPublic = true,
            },
            new DesktopCaseProduct
            {
                WarantyMonths = 24,
                Widthmm = 215,
                Description = "Zapewnij swojemu desktopowi niepowtarzalną oprawę z czarną obudową MPG Gungnir 110M. Ta oryginalna konstrukcja wyróżnia się agresywnym gamingowym designem, uzupełnionym przez przeszklony panel boczny i statyczne podświetlenie LED. Możesz z dumą wyeksponować stworzoną konfigurację i ukazać desktopa skąpanego w wielokolorowym blasku. Przestronne wnętrze pomieści wszystkie niezbędne komponenty, a fabryczny system chłodzenia zapewni odpowiednią wentylację.",
                Heightmm = 480,
                Images = new List<Image>{new Image { Location= "https://cdn.x-kom.pl/i/setup/images/prod/big/product-new-big,,2020/7/pr_2020_7_8_11_36_31_947_00.jpg" } },
                Lenghtmm = 430,
                Manufacturer = "MSI",
                MaxCoolerHeightmm = 170,
                MaxFanCount = 6,
                MaxGpuLenghtmm = 340,
                MaxRadiatorSizemm = 140,
                Name = "MPG Gungnir 110M",
                Price = 539,
                PriceBeforeDiscount = 539,
                SupportedMoboSizes = new List<string>{ MotherboardSizes.MicroATX, MotherboardSizes.ExtendedAtx, MotherboardSizes.StandardAtx},
                UsbPorts = 3,
                IsPublic = true,
            },
new DesktopCaseProduct
            {
                WarantyMonths = 24,
                Widthmm = 221,
                Description = "Dzięki doskonale przemyślanej konstrukcji SilentiumPC Regnum RG6V EVO TG RGB pozwala na montaż licznych komponentów. Akcesorium zostało wyposażone w przezroczysty panel boczny oraz panel przedni z siateczki mesh, co sprawia, że możesz ukazać swoją gamingową konfigurację w jej najlepszych barwach.",
                Heightmm = 470,
                Images = new List<Image>{new Image { Location= "https://cdn.x-kom.pl/i/setup/images/prod/big/product-new-big,,2020/1/pr_2020_1_27_13_57_7_455_13.jpg" } },
                Lenghtmm = 443,
                Manufacturer = "SilentiumPC",
                MaxCoolerHeightmm = 162,
                MaxFanCount = 7,
                MaxGpuLenghtmm = 360,
                MaxRadiatorSizemm = 140,
                Name = "Regnum RG6V EVO TG ARGB",
                Price = 389,
                PriceBeforeDiscount = 389,
                SupportedMoboSizes = new List<string>{ MotherboardSizes.MicroATX, MotherboardSizes.ExtendedAtx, MotherboardSizes.StandardAtx},
                UsbPorts = 2,
                IsPublic = true,
            },
new DesktopCaseProduct
            {
                WarantyMonths = 36,
                Widthmm = 232,
                Description = "Zachwycająca swoim oryginalnym wyglądem obudowa Pure Base 500DX pozwala na montaż wielu komponentów. Dodatkowo została zoptymalizowana pod kątem zapewnienia dobrego przepływu powietrza, aby Twój system działał sprawnie. Wbudowane podświetlenie podkreśli agresywny charakter konfiguracji, a przezroczysty panel boczny umożliwi Ci pokazanie wnętrza obudowy.",
                Heightmm = 463,
                Images = new List<Image>{new Image { Location= "https://cdn.x-kom.pl/i/setup/images/prod/big/product-new-big,,2020/4/pr_2020_4_23_11_50_6_672_00.jpg" } },
                Lenghtmm = 450,
                Manufacturer = "be quiet!",
                MaxCoolerHeightmm = 190,
                MaxFanCount = 3,
                MaxGpuLenghtmm = 369,
                MaxRadiatorSizemm = 140,
                Name = "Pure Base 500DX Black",
                Price = 499,
                PriceBeforeDiscount = 499,
                SupportedMoboSizes = new List<string>{ MotherboardSizes.MicroATX, MotherboardSizes.ExtendedAtx, MotherboardSizes.StandardAtx},
                UsbPorts = 2,
                IsPublic = true,
            },
new DesktopCoolerProduct
            {
                WarantyMonths= 72,
                CompatibleSockets = new(){"AM4", "AM3", "AM3+", "AM2+", "FM2+", "FM1", "2066", "2011", "1700", "1200"},
                FansCount = 1,
                CoolerType = CoolerTypes.BoxCooler,
                Manufacturer = "SilentiumPC",
                MaxTdp = 220,
                Name = "Fortis 5 140mm",
                Price = 189,
                PriceBeforeDiscount = 189,
                Sizemm = 159,
                Description = "Fortis 5 to nowy układ chłodzenia procesora od SilentiumPC, opracowany we współpracy z Synergy Cooling. Wyróżnia go zupełnie nowy, asymetryczny w dwóch osiach radiator z gęsto upakowanymi finami, zoptymalizowana podstawa z sześcioma ciepłowodami, a także zupełnie nowy wentylator Fluctus zoptymalizowany pod kątem ciśnienia statycznego, wysokiego przepływu powietrza i cichej pracy.",
                IsPublic = true,
            },
new DesktopCoolerProduct
            {
                WarantyMonths= 24,
                CompatibleSockets = new(){"AM4", "AM3", "AM3+", "AM2+", "FM2+", "FM1", "2066", "2011", "1700", "1200"},
                FansCount = 2,
                CoolerType = "Wodne",
                Manufacturer = "Cooler Master",
                MaxTdp = 200,
                Name = "MasterLiquid ML240L V2 RGB 2x120mm",
                Price = 329,
                PriceBeforeDiscount = 329,
                Sizemm = 277,
                Description = "Ten zamknięty zestaw chłodzenia wodnego typu All-In-One jest wyjątkowo łatwy w instalacji, a przy tym nie wymaga specjalnej konserwacji. MasterLiquid ML240L V2 to rozwiązanie zgodne z wieloma podstawkami Intel oraz AMD, oferujące jednocześnie unikalny design z podświetleniem RGB. Zapewnij swojemu procesorowi najlepsze odprowadzanie ciepła, wykorzystując gotowy układ chłodzenia cieczą MasterLiquid ML240L V2.",
                IsPublic = true,
            },
new DesktopCoolerProduct
            {
                WarantyMonths= 36,
                CompatibleSockets = new(){"AM4", "1700", "1200", "1150", "1151", "1155", "2011", "2066"},
                FansCount = 2,
                CoolerType = CoolerTypes.BoxCooler,
                Manufacturer = "bequite",
                MaxTdp = 250,
                Name = "Dark Rock Pro 4 120/135mm",
                Price = 389,
                PriceBeforeDiscount = 389,
                Sizemm = 163,
                Description = "Dark Rock Pro 4 to wyjątkowy układ chłodzenia, należący do najbardziej zaawansowanej serii wśród coolerów CPU od be quiet!. Oferuje niezrównaną wydajność z TDP na poziomie 250W i praktycznie niesłyszalną pracą. Doskonale nadaje się do ​​przetaktowanych systemów i wymagających stacji roboczych, jednocześnie oferując wyjątkowy, gamingowy design. Łatwy w montażu zestaw instalacyjny umożliwia wykorzystanie Dark Rock Pro 4 na wielu podstawkach procesorów, zarówno Intel, jak i AMD.",
                IsPublic = true,
            },
new CpuProduct
            {
                WarantyMonths = 36,
                CoresCount = 6,
                Description = "Jednostka AMD Ryzen 5 5600X posiada 6 rdzeni i 12 wątków, gotowych do pracy przy maksymalnym obciążeniu w grach i specjalistycznych aplikacjach. Pamięć cache tego procesora liczy łącznie 35 MB, a bazowe taktowanie rdzeni otwiera częstotliwość 3,70 GHz, mogąca sięgać aż do 4,60 GHz w trybie Turbo. Moc jednostki może jednak wzrosnąć dzięki odblokowanej możliwości podkręcania. Czas na ogromną moc wspartą przez nowatorską architekturę Zen 3.",
                FrequencyMHz = 3700,
                Images = new List<Image>{new Image { Location= "https://d2csxpduxe849s.cloudfront.net/media/final/b1437a6d-7b11-43f8-bec2-0e97d9f91900/webimage-63FE5CAF-67CD-4B0B-80002B54CB6090E1.jpg" } },
                L3CacheMB = 35,
                Manufacturer = "AMD",
                Name = "5 5600X",
                Price = 1069,
                PriceBeforeDiscount = 1160,
                SupportedChipsets = new(){"B450, B550, A520, X470, X570"},
                SupportedSocket = "AM4",
                Tdp = 65,
                ThreadsCount = 12,
            },
new CpuProduct
            {
                WarantyMonths = 36,
                CoresCount = 6,
                Description = "Dzięki 11. architekturze procesorów Intel Core do komputerów stacjonarnych i odpowiedniej równowadze między taktowaniem nawet 4,4 GHz a potężnymi 6 rdzeniami i 12 wątkami, możesz uzyskać dużą liczbę klatek i niewielkie opóźnienia, aby granie w najnowsze produkcje sprawiało większą przyjemność. Dzięki zaawansowanym funkcjom oraz obsłudze najnowszych platform technologicznych, z procesorem Intel Core i5-11400F 11. generacji możesz bawić się tak, jak chcesz.",
                FrequencyMHz = 4600,
                Images = new List<Image>{new Image { Location= "https://www.intel.com/content/dam/www/public/us/en/images/logos/logo-rwd.png.rendition.intel.web.1648.927.png" } },
                L3CacheMB = 12,
                Manufacturer = "Intel",
                Name = "i5-11400F",
                Price = 719,
                PriceBeforeDiscount = 719,
                SupportedChipsets = new(){"H410, H510, B460, B560, Z490, Z590"},
                SupportedSocket = "1200",
                Tdp = 65,
                ThreadsCount = 12,
            },
new CpuProduct
            {
                WarantyMonths = 36,
                CoresCount = 8,
                Description = "Procesor AMD Ryzen 7 3800X wprowadzi Cię na nowym poziom wydajności. Ten potężny CPU posiada 8 rdzeni o bazowej częstotliwości taktowania zegarów 3,90 GHz. W trybie Turbo przyspiesza ono do 4,50 GHz, zapewniając ogromną moc do płynnego gamingu oraz bezbłędny multitasking zaawansowanych operacji. Jednostka czerpie z całego spektrum możliwości architektury Zen 2, jej chłodzeniem zajmuje się wentylator Wraith Prism z oświetleniem RGB LED.",
                FrequencyMHz = 3900,
                Images = new List<Image>{new Image { Location= "https://d2csxpduxe849s.cloudfront.net/media/final/b1437a6d-7b11-43f8-bec2-0e97d9f91900/webimage-63FE5CAF-67CD-4B0B-80002B54CB6090E1.jpg" }, new Image { Location= "https://d2csxpduxe849s.cloudfront.net/media/final/b1437a6d-7b11-43f8-bec2-0e97d9f91900/webimage-63FE5CAF-67CD-4B0B-80002B54CB6090E1.jpg" }   },
                L3CacheMB = 36,
                Manufacturer = "AMD",
                Name = "7 3800x",
                Price = 1349,
                PriceBeforeDiscount = 1399,
                SupportedChipsets = new(){"A520, B450, B550, X570, X470"},
                SupportedSocket = "AM4",
                Tdp = 105,
                ThreadsCount = 16,
            },
new KeyboardProduct
            {
                Weightg = 1000,
                IsWireless = false,
                Widthmm = 218,
                Color = "black",
                KeyboardType = "Membranowa",
                Manufacturer = "Logitech",
                Price = 179,
                Size = "Full",
                Name = "G213 PRODIGY",
                WarantyMonths= 24,
                Heightmm = 33,
                Images = new(){ new Image{Location = "https://cdn.x-kom.pl/i/setup/images/prod/big/product-new-big,,2018/8/pr_2018_8_23_10_45_55_912_00.jpg"} },
                Description = "Logitech G213 PRODIGY to ergonomiczna klawiatura gamingowa, którą charakteryzuje wysoka jakość wykonania, niezawodność i szybki czas reakcji. To cechy, dzięki którym odniesiesz sukces. Jej funkcjonalność oraz konstrukcja dostosowana jest typowo do gamingu. Klawiatura Logitech G213 PRODIGY pomoże Ci odmienić losy rozgrywki na Twoją korzyść.",
                IsPublic = true,
                Interface = "USB",
                Lenghtmm = 452,
            },
new KeyboardProduct
            {
                Weightg = 816,
                IsWireless = false,
                Widthmm = 152,
                Color = "black",
                KeyboardType = "Membranowa",
                Manufacturer = "SteelSeries",
                Price = 309,
                Size = "Full",
                Name = "Apex 3",
                WarantyMonths= 24,
                Heightmm = 40,
                Images = new(){ new Image{Location = "https://cdn.x-kom.pl/i/setup/images/prod/big/product-new-big,,2020/3/pr_2020_3_4_10_6_46_530_00.jpg"} },
                Description = "Przygotuj się do gry w najlepszy możliwy sposób z klawiaturą SteelSeries Apex 3. Zaprojektowano ją tak, aby łączyła w sobie nowoczesne technologie z użytecznymi funkcjonalnościami gamingowymi. Dzięki temu każda rozgrywka dostarczy Ci niesamowitych wrażeń. Klawiatura wyposażona jest w przełączniki zmniejszające tarcie, gwarantujące ponadprzeciętną trwałość. Zamów swój egzemplarz i wejdź do gry.",
                IsPublic = true,
                Interface = "USB",
                Lenghtmm = 445,
            },
new KeyboardProduct
            {
                Weightg = 911,
                IsWireless = false,
                Widthmm = 153,
                Color = "black",
                KeyboardType = "Membranowa",
                Manufacturer = "Razer",
                Price = 339,
                Size = "Full",
                Name = "Ornata V2",
                WarantyMonths= 24,
                Heightmm = 32,
                Images = new(){ new Image{Location = "https://cdn.x-kom.pl/i/setup/images/prod/big/product-new-big,,2020/7/pr_2020_7_2_11_27_43_584_00.jpg"} },
                Description = "Razer Ornata V2 to ergonomiczna klawiatura gamingowa, którą charakteryzuje wysoka jakość wykonania, niezawodność i szybki czas reakcji. To cechy, dzięki którym odniesiesz sukces. Jej funkcjonalność oraz konstrukcja dostosowana jest typowo do gamingu. Klawiatura Razer Ornata V2 pomoże Ci odmienić losy rozgrywki na Twoją korzyść.",
                IsPublic = true,
                Interface = "USB",
                Lenghtmm = 432,
            },
new HeadphonesProduct
            {
                Weightg = 325,
                Widthmm = 200,
                ImpedanceOhm = 32,
                Manufacturer = "SteelSeries",
                Name = "Arctis 5 Czarne",
                PriceBeforeDiscount = 469,
                Price = 399,
                MaxFrequencyResponsekHz = 22000,
                MinFrequencyResponseHz = 22,
                Interface = "Jack",
                HeadphonesType = "Zakmniete",
                WarantyMonths = 24,
                IsWireless= true,
                Description = "Inspiracją do stworzenia poduszek wewnątrz nauszników były stroje sportowe. Stworzone przez firmę SteelSeries poduszki o nazwie AirWeave są niezwykle miękkie i przyjemne w dotyku. Pamiętająca kształt pianka została umieszczona wewnątrz oddychającego, pochłaniającego wilgoć materiału. Materiał ten został dodatkowo pokryty termoplastyczną powłoką zapewniającą skuteczną izolację szumów.",
                Heightmm = 330,
                Images = new(){ new Image{Location = "https://cdn.x-kom.pl/i/setup/images/prod/big/product-new-big,,2018/11/pr_2018_11_9_12_53_19_908_03.jpg"} },
                IsPublic = true,
                Lenghtmm = 300,
                IsHiglighted = true,
            },
new HeadphonesProduct
            {
                Weightg = 305,
                Widthmm = 199,
                ImpedanceOhm = 32,
                Manufacturer = "KFA2",
                Name = "SONAR-04",
                PriceBeforeDiscount = 129,
                Price = 59,
                MaxFrequencyResponsekHz = 20000,
                MinFrequencyResponseHz = 20,
                Interface = "Jack",
                HeadphonesType = "Zamkniete",
                WarantyMonths = 24,
                IsWireless= true,
                Description = "KFA2 SONAR-04 to gamingowy zestaw słuchawkowy, który zachwyci Cię jakością oferowanego dźwięku oraz wygodą użytkowania nawet przez wiele godzin. Wytrzymałość gwarantuje między innymi wzmacniany pałąk wykonany z polimeru.",
                Heightmm = 327,
                Images = new(){ new Image{Location = "https://cdn.x-kom.pl/i/setup/images/prod/big/product-new-big,,2022/1/pr_2022_1_7_14_20_48_412_05.jpg"} },
                IsPublic = true,
                Lenghtmm = 295,
                IsHiglighted = true,
            },
new HeadphonesProduct
            {
                Weightg = 307,
                Widthmm = 201,
                ImpedanceOhm = 32,
                Manufacturer = "SPC",
                Name = "VIRO",
                PriceBeforeDiscount = 185,
                Price = 185,
                MaxFrequencyResponsekHz = 10000,
                MinFrequencyResponseHz = 100,
                Interface = "Jack",
                HeadphonesType = "Zamkniete",
                WarantyMonths = 24,
                IsWireless= true,
                Description = "SPC Gear VIRO to przeznaczone dla graczy przewodowe i wokółuszne słuchawki z dynamicznymi przetwornikami i odpinanym mikrofonem. Precyzyjne ujęcie szczegółów i kierunku w grach, skupienie się na detalach takich jak kroki wirtualnego przeciwnika, a także szeroka scena to najważniejsze cechy akustycznego dostrojenia słuchawek VIRO.",
                Heightmm = 321,
                Images = new(){ new Image{Location = "https://cdn.x-kom.pl/i/setup/images/prod/big/product-new-big,,2020/7/pr_2020_7_16_13_55_58_462_00.jpg"} },
                IsPublic = true,
                Lenghtmm = 290,
                IsHiglighted = true,
            },
new HddProduct
            {
                WriteSpeedMBs = 120,
                CapacityGB = 2000,
                Description = "Uniwersalny dysk Seagate Barracuda to gwarancja sprawnego dostępu do Twoich plików. Wykorzystaj jego przestrzeń do przechowywania danych niezbędnych do pracy i zyskaj wygodne narzędzie, które wspomoże w wykonywaniu codziennych obowiązków. Dysk ten posłuży Ci także do przechowywania zbioru zdjęć z wakacji, filmów, ulubionej muzyki oraz gier.",
                Images = new List<Image>{new Image { Location= "https://cdn.x-kom.pl/i/setup/images/prod/big/product-new-big,,2019/9/pr_2019_9_10_9_41_46_776_03.jpg" } },
                Manufacturer = "Seagate",
                ReadSpeedMBs = 233,
                Name = "BARRACUDA 2TB 7200obr. 256MB ",
                Price = 259,
                Rpm = 7200,
                WarantyMonths = 24,
                Interface = "SATA 3", //hddki mają tylko sata 3, a ssd to ablo sata 3 ablo M.2
                Type = "PC",
                IsPublic = true,
            },
new HddProduct
            {
                WriteSpeedMBs = 150,
                CapacityGB = 1000,
                Description = "Toshiba P300 to szybki, niezawodny i wszechstronny dysk. Jeżeli potrzebujesz dodatkowej przestrzeni na zdjęcia, filmy, gry, czy muzykę, to znalazłeś produkt idealny dla siebie. Ten uniwersalny dysk sprawdzi się idealnie jako magazyn plików dla Twojego domowego komputera. Wysoka prędkość obrotów pozwala na płynny dostęp do folderów oraz niezakłócone działanie podczas pracy.",
                Images = new List<Image>{new Image { Location= "https://cdn.x-kom.pl/i/setup/images/prod/big/product-new-big,,2017/7/pr_2017_7_26_14_15_32_657.jpg" } },
                Manufacturer = "Toshiba",
                ReadSpeedMBs = 155,
                Name = "P300 1TB 7200obr. 64MB OEM",
                Price = 175,
                Rpm = 7200,
                WarantyMonths = 24,
                Interface = "SATA 3", //hddki mają tylko sata 3, a ssd to ablo sata 3 ablo M.2
                Type = "PC",
                IsPublic = true,
            },
new HddProduct
            {
                WriteSpeedMBs = 150,
                CapacityGB = 40,
                Description = "Jeżeli szukasz uniwersalnego dysku dla swojego komputera, to WD Blue Cię nie zawiedzie. Teraz wreszcie znajdziesz dość miejsca na swoją kolekcję zdjęć oraz filmów, jak i ulubione gry, czy dokumenty potrzebne do pracy. Wybierając dysk WD Blue wykonywanie codziennych czynności na Twoim komputerze PC stanie się szybkie i przyjemne.",
                Images = new List<Image>{new Image { Location= "https://cdn.x-kom.pl/i/setup/images/prod/big/product-new-big,,2021/4/pr_2021_4_8_11_54_42_265_00.jpg" } },
                Manufacturer = "WD",
                ReadSpeedMBs = 150,
                Name = "1TB 7200obr. 64MB CMR",
                Price = 205,
                Rpm = 7200,
                WarantyMonths = 24,
                Interface = "SATA 3", //hddki mają tylko sata 3, a ssd to ablo sata 3 ablo M.2
                Type = "PC",
                IsPublic = true,
            },new DesktopGpuProduct
            {
                WarantyMonths=24,
                ChipManufacturer = GpuManufacturers.Nvidia.ToString(),
                Description = " Graj na najwyższym poziomie ze stworzoną dla graczy kartą graficzną Gigabyte GeForce RTX 3070 Ti GAMING OC. Ta zbudowana w oparciu o architekturę NVIDIA Ampere konstrukcja zapewnia wysoką wydajność oraz wsparcie dla wielu technologii, takich jak Ray Tracing czy DLSS. Stylowy design karty dopełnia podświetlane logo Gigabyte zgodne z RGB Fusion 2.0. Podwójny układ BIOS umożliwia łatwe przełączanie się pomiędzy trybem cichym a trybem OC. A z pomocą oprogramowania Aorus Engine możesz monitorować stan karty oraz dodatkowo podkręcać jej osiągi.",
                FrequencyMHz = 1830,
                Images = new(){ new Image { Location = "https://www.gigabyte.com/FileUpload/Global/KeyFeature/1871/innergigabyteimages/eagle/08.jpg"},
                                new Image { Location = "https://www.gigabyte.com/FileUpload/Global/KeyFeature/1871/innergigabyteimages/eagle/03.jpg"},},
                IsPublic = true,
                Lenghtmm = 320,
                Manufacturer = "Gigabyte",
                MemoryFrequencyMHz = 19000,
                Price = 3999,
                PriceBeforeDiscount = 3999,
                Name = "GeForce RTX 3070 Ti GAMING OC 8GB GDDRX6",
                Tdp = 250,
                VramSizeGB = 8,
                VramType = "GDDR6X",
                BusWidth = 256, //coś z 256 128 512 196
                PortsList = new(){"HDMI", "Display Port"}, //to może być dla wszystkich takie samo
                PowerConnectors = new(){ "PCI-E 8 pin", "PCI-E 8 pin"},
            },
new DesktopGpuProduct
            {
                WarantyMonths=36,
                ChipManufacturer = GpuManufacturers.Nvidia.ToString(),
                Description = "Jeżeli preferujesz minimalistyczny design pozbawiony podświetlenia RGB, to karta MSI GeForce RTX 3080 Ti VENTUS 3X OC jest rozwiązaniem dla Ciebie. Dzięki architekturze NVIDIA Ampere oraz 12 GB pamięci GDDR6X z powodzeniem zagrasz w najnowsze produkcje, mogąc cieszyć się wysokim poziomem FPS oraz fotorealistyczną grafiką. Wszystko to dzięki nowatorskim technologiom, takim jak Ray Tracing czy DLSS. A temperatury GPU w ryzach utrzyma wydajny układ chłodzenia z wentylatorami TORX 3.0.",
                FrequencyMHz = 1695,
                Images = new(){ new Image { Location = "https://asset.msi.com/resize/image/global/product/product_1622528104a426e394a9f5e0b4afd271ac25b30c3e.png62405b38c58fe0f07fcef2367d8a9ba1/1024.png"},
                                new Image { Location = "https://asset.msi.com/resize/image/global/product/product_162252810628fe05c4dd06745553e422473b2d3bf2.png62405b38c58fe0f07fcef2367d8a9ba1/1024.png"},},
                IsPublic = true,
                Lenghtmm = 305,
                Manufacturer = "MSI",
                MemoryFrequencyMHz = 19000,
                Price = 6899,
                PriceBeforeDiscount = 7499,
                Name = "GeForce RTX 3080 Ti VENTUS 3X OC 12GB GDDR6X",
                Tdp = 200,
                VramSizeGB = 12,
                VramType = "GDDR6X",
                BusWidth = 384, //coś z 256 128 512 196
                PortsList = new(){"HDMI", "Display Port"}, //to może być dla wszystkich takie samo
                PowerConnectors = new(){ "PCI-E 8 pin", "PCI-E 8 pin"},
            },
new DesktopGpuProduct
            {
                WarantyMonths=36,
                ChipManufacturer = GpuManufacturers.Nvidia.ToString(),
                Description = "Skorzystaj z połączenia osiągów architektury NVIDIA Ampere oraz niezawodności serii TUF. Karta graficzna ASUS GeForce RTX 3070 Ti TUF Gaming OC to unikalna konstrukcja zapewniająca wysoką wydajność oraz wsparcie dla technologii takich jak Ray Tracing czy DLSS. Nad optymalnymi temperaturami czuwa układ chłodzenia z trzema wentylatorami Axial-Tech. A podwójny BIOS pozwala Ci łatwo przełączać się pomiędzy trybem cichej pracy a maksymalnej wydajności.",
                FrequencyMHz = 1900,
                Images = new(){ new Image { Location = "https://dlcdnwebimgs.asus.com/gain/30001b95-4733-472c-b799-a0c32170c288/w692"},},
                IsPublic = true,
                Lenghtmm = 300,
                Manufacturer = "Asus",
                MemoryFrequencyMHz = 19000,
                Price = 4399,
                PriceBeforeDiscount = 4399,
                Name = "GeForce RTX 3070 Ti TUF Gaming OC LHR 8GB GDDR6X",
                Tdp = 300,
                VramSizeGB = 8,
                VramType = "GDDR6X",
                BusWidth = 256, //coś z 256 128 512 196
                PortsList = new(){"HDMI", "Display Port"}, //to może być dla wszystkich takie samo
                PowerConnectors = new(){ "PCI-E 8 pin", "PCI-E 8 pin"},
            },
new DesktopGpuProduct
            {
                WarantyMonths=36,
                ChipManufacturer = GpuManufacturers.Amd.ToString(),
                Description = "Skoncentrowana na wydajności karta graficzna MSI Radeon RX 6600 XT MECH 2X OC 8GB GDDR6 oferuje wyłącznie to, co jest niezbędne do wykonania każdego postawionego przed nią zadania. W minimalistycznej obudowie zamknięto możliwości nowatorskiej architektury AMD RDNA 2, w tym wsparcie dla sprzętowego Ray Tracingu. Możesz więc cieszyć się fotorealistyczną grafiką oraz płynną rozgrywką w najnowszych tytułach. Wydajne chłodzenie zadba o optymalne temperatury pracy nawet w ogniu najgorętszej rozgrywki.",
                FrequencyMHz = 2602,
                Images = new(){ new Image { Location = "https://asset.msi.com/resize/image/global/product/product_16276104188b8e1e4ebf019790c2005b1f5603f29e.png62405b38c58fe0f07fcef2367d8a9ba1/1024.png"},
                                new Image { Location = "https://asset.msi.com/resize/image/global/product/product_1627610420f3dbf78176ecf62d947cf6dac2aa8e06.png62405b38c58fe0f07fcef2367d8a9ba1/1024.png"},},
                IsPublic = true,
                Lenghtmm = 235,
                Manufacturer = "MSI",
                MemoryFrequencyMHz = 16000,
                Price = 2200,
                PriceBeforeDiscount = 2499,
                Name = "Radeon RX 6600 XT MECH 2X OC 8GB GDDR6",
                Tdp = 200,
                VramSizeGB = 8,
                VramType = "GDDR6",
                BusWidth = 128, //coś z 256 128 512 196
                PortsList = new(){"HDMI", "Display Port"}, //to może być dla wszystkich takie samo
                PowerConnectors = new(){ "PCI-E 8 pin"},
            },
new SsdProduct
            {
                WriteSpeedMBs = 3000,
                CapacityGB = 1000,
                Description = "Dysk Samsung NVMe SSD 980 to pierwszy dysk konsumencki firmy Samsung bez pamięci DRAM. Wyjątkowe parametry urządzenia gwarantują niezwykłą wydajność i wielokrotnie wyższą prędkość niż oferują dyski SATA SSD, dostępną od teraz dla szerszego grona użytkowników. Łączy w sobie szybkość, energooszczędność i niezawodność, którą docenisz podczas codziennego użytkowania, dynamicznej rozgrywki na najwyższych parametrach oraz pracy przy dużych plikach.",
                Images = new List<Image>{new Image { Location= "https://cdn.x-kom.pl/i/setup/images/prod/big/product-new-big,,2021/3/pr_2021_3_19_14_11_3_537_03.jpg" } },
                Interface = "M.2", //hddki mają tylko sata 3, a ssd to ablo sata 3 ablo M.2
                Manufacturer = "Samsung",
                ReadSpeedMBs = 3500,
                Name = "1TB M.2 PCIe NVMe 980",
                Tbw = 500,
                Price = 529,
                IsPublic = true,
            },
new SsdProduct
            {
                WriteSpeedMBs = 510,
                CapacityGB = 500,
                Description = "Za każdym razem, gdy uruchamiasz swój komputer, używasz zamontowanego w nim dysku twardego, przechowując tam dane systemowe oraz najważniejsze pliki. Dołącz do użytkowników technologii SSD, wykorzystując dysk SSD Crucial MX500 stworzony przez firmę Micron. Przyspiesz pracę swojego komputera, jednocześnie zmniejszając hałas i zużycie energii. Możesz być przy tym spokojny o swoje dane, dzięki wielu zaawansowanych technologiom, gwarantującym nie tylko niezmienne osiągi, ale także wyjątkowe bezpieczeństwo i niezawodność.",
                Images = new List<Image>{new Image { Location= "https://cdn.x-kom.pl/i/setup/images/prod/big/product-new-big,,2018/1/pr_2018_1_3_14_2_15_279_01.jpg" } },
                Interface = "SATA", //hddki mają tylko sata 3, a ssd to ablo sata 3 ablo M.2
                Manufacturer = "Crucial",
                ReadSpeedMBs = 560,
                Name = "500GB 2,5 cala SATA SSD MX500",
                Tbw = 500,
                Price = 269,
                IsPublic = true,
            },
new SsdProduct
            {
                WriteSpeedMBs = 450,
                CapacityGB = 1000,
                Description = "ADATA Ultimate SU650 to dysk półprzewodnikowy o pojemności 256 GB, który wykorzystuje pamięć Flash NAND 3D i kontroler wysokiej prędkości. Dysk ten zapewnia wydajność odczytu i zapisu danych na poziomie odpowiednio do 520 / 450 MB/s, a także większą niezawodność niż dyski SSD NAND 2D.",
                Images = new List<Image>{new Image { Location= "https://cdn.x-kom.pl/i/setup/images/prod/big/product-new-big,,2022/3/pr_2022_3_15_7_39_59_759_00.jpg" } },
                Interface = "SATA", //hddki mają tylko sata 3, a ssd to ablo sata 3 ablo M.2
                Manufacturer = "ADATA",
                ReadSpeedMBs = 520,
                Name = "256GB 2,5 cala SATA SSD Ultimate SU650",
                Tbw = 500,
                Price = 129,
                IsPublic = true,
            },
new RamProduct
            {
                LatencyCL = 16,
                Description = "Gamingowa pamięć RAM Crucial Ballistix została zaprojektowana z myślą o zapewnieniu wysokiej wydajności w nowoczesnych grach, jak również podczas zaawansowanego podkręcania. Doskonale współpracuje z platformami Intel oraz AMD, wspierając przy tym obsługę profili XMP 2.0.",
                FrequencyMHz = 3200,
                Images = new(){new Image {Location="https://cdn.x-kom.pl/i/setup/images/prod/big/product-new-big,,2020/3/pr_2020_3_10_14_46_46_355_00.jpg"}},
                ModulesNumber = 2,
                RamTechnology = RamTechnologies.DDR4.ToString(),
                Name = "Ballistix 16GB DDR4",
                Manufacturer = "Crucial",
                Price= 359,
                PriceBeforeDiscount = 359,
                IsPublic = true,
            },
new RamProduct
            {
                LatencyCL = 17,
                Description = "Niezależnie od tego, czy korzystasz z platformy Intel lub AMD, Twoja konfiguracja potrzebuje najlepszej i najszybszej pamięci RAM. Stworzona z myślą o prawdziwej wydajności seria Viper Steel stawi czoło obciążeniom, jakimi będzie poddawana nawet w najbardziej wymagających środowiskach komputerowych. Drapieżny design doskonale odnajdzie się w Twojej obudowie, a taktowanie 3600 MHz oraz opóźnienia CL17 zapewnią płynną obsługę gier oraz rozmaitych aplikacji.",
                FrequencyMHz = 3600,
                Images = new(){new Image {Location="https://cdn.x-kom.pl/i/setup/images/prod/big/product-new-big,,2019/2/pr_2019_2_1_13_8_17_828_00.jpg"}},
                ModulesNumber = 2,
                RamTechnology = RamTechnologies.DDR4.ToString(),
                Name = "16GB (2x8GB) 3600MHz CL17 Viper Steel",
                Manufacturer = "Patriot",
                Price= 359,
                PriceBeforeDiscount = 379,
                IsPublic = true,
            },
new RamProduct
            {
                LatencyCL = 17,
                Description = "Zmodernizuj swój system dzięki stylowej i niezwykle wydajnej pamięci RAM DDR4 Kingston FURY Beast RGB. Dwa moduły o łącznej pojemności 16 GB (2x8GB) oferują taktowanie 3600 MHz oraz opóźnienia na poziomie CL17. Ponadto moduły Kingston FURY Beast RGB wspierają obsługę profili XMP.",
                FrequencyMHz = 3600,
                Images = new(){new Image {Location="https://cdn.x-kom.pl/i/setup/images/prod/big/product-new-big,,2021/7/pr_2021_7_9_14_28_8_356_02.jpg"}},
                ModulesNumber = 2,
                RamTechnology = RamTechnologies.DDR4.ToString(),
                Name = "FURY 16GB (2x8GB) 3600MHz CL17 Beast RGB",
                Manufacturer = "Kingston",
                Price= 459,
                PriceBeforeDiscount = 449,
                IsPublic = true,
            },
new DesktopPsuProduct
            {
                Manufacturer = PsuManufacturers.SilentiumPc.ToString(),
                Name = "Vero L3 700W 80 Plus Bronze",
                WarantyMonths = 36,
                FansCount = 1,
                Description = "Zasilacz ATX Vero L3 700 W to bardzo wysoka sprawność, potwierdzona certyfikatem 80 PLUS Bronze 230V, nowoczesne i wydajne konwertery DC-DC, bardzo mocna pojedyncza linia +12 V, jak również bardzo ciche działanie w dużym zakresie obciążenia, a także bogaty zestaw płaskiego i czarnego okablowania. Wysokie parametry zasilaczy komputerowych Vero L3 to efekt użycia w ich budowie sprawdzonych komponentów wysokiej klasy, znanych dotychczas raczej z wyższych segmentów.",
                Certificate = PsuCertificates.Bronze,
                MolexCount = 2,
                Images = new List<Image>
                {
                    new Image { Location = "https://www.silentiumpc.com/wp-content/uploads/2021/02/spc267-spc-vero-L3-bronze-700-01-png-www.png"},
                },
                Power = 700,
                Price = 289,
                SataCount = 7,
                Pcie6pinCount = 3,
                Protections = PsuProtections.List,
                ExtraInfo = new() //jak chcesz to Extra info możesz wywalić jak nie masz pomysłu co tu można dodać
                {
                    new KeyValue{Key = "Kolor", Value = "Czarny"},
                },
                IsModular = false,
                IsPublic = true,
                Pcie8pinCount = 3,
                PriceBeforeDiscount = 289, //ceny przed obniżką nie musisz dawać na wszystkich
            },
new DesktopPsuProduct
            {
                Manufacturer = PsuManufacturers.beQuiet.ToString(),
                Name = "Straight Power 11 850W 80 Plus Gold",
                WarantyMonths = 60,
                FansCount = 1,
                Description = "W najbardziej zaawansowanych systemach nie ma miejsca na eksperymenty. Wybierz zasilacz be quiet! Straight Power 11 850 W, gwarantujący niezachwianą stabilność oraz supercichą pracę chłodzenia. Komponent posiada w pełni modularne zarządzanie przewodami, co ułatwia prowadzenie przewodów i umożliwia maksymalną elastyczność konfiguracji. Zasilacz Straight Power 11 850 W dysponuje również czterema, wysokowydajnymi liniami 12V oraz japońskimi kondensatorami. Ich obecność przekłada się dłuższą żywotność, a także niezawodność. Z myślą o mocarnych zestawach zasilacz wzbogacono o wsparcie dla najmocniejszych GPU z dwoma złączami PCI-e.",
                Certificate = PsuCertificates.Gold,
                MolexCount = 4,
                Images = new List<Image>
                {
                    new Image { Location = "https://www.bequiet.com/admin/ImageServer.php?ID=ee264a11774@be-quiet.net&omitPreview=true&.jpg"},
                },
                Power = 850,
                Price = 600,
                SataCount = 11,
                Pcie6pinCount = 4,
                Protections = PsuProtections.List,
                ExtraInfo = new() //jak chcesz to Extra info możesz wywalić jak nie masz pomysłu co tu można dodać
                {
                    new KeyValue{Key = "Kolor", Value = "Czarny"},
                },
                IsModular = true,
                IsPublic = true,
                Pcie8pinCount = 4,
                PriceBeforeDiscount = 699, //ceny przed obniżką nie musisz dawać na wszystkich
            },
new DesktopPsuProduct
            {
                Manufacturer = PsuManufacturers.Corsair.ToString(),
                Name = "RMx White 750W 80 Plus Gold",
                WarantyMonths = 120,
                FansCount = 1,
                Description = "Wyjątkowe zasilacze Corsair z serii RMx oferują ścisłą kontrolę napięcia, cichą pracę oraz w pełni modularne okablowanie. Model RM 750x White zbudowany został z japońskich kondensatorów 105°C, dlatego jest doskonałym wyborem dla komputerów potrzebujących wydajności i niezawodności. Matowobiały kolor obudowy oraz biały oplot przewodów umożliwiły uzyskać minimalistyczny i nowoczesny wygląd jednostki.",
                Certificate = PsuCertificates.Gold,
                MolexCount = 8,
                Images = new List<Image>
                {
                    new Image { Location = "https://www.corsair.com/medias/sys_master/images/images/hb4/hdb/9078594076702/-CP-9020187-EU-Gallery-RMx-White-750-01.png"},
                },
                Power = 750,
                Price = 599,
                SataCount = 9,
                Pcie6pinCount = 4,
                Protections = PsuProtections.List,
                ExtraInfo = new() //jak chcesz to Extra info możesz wywalić jak nie masz pomysłu co tu można dodać
                {
                    new KeyValue{Key = "Kolor", Value = "Biały"},
                },
                IsModular = true,
                IsPublic = true,
                Pcie8pinCount = 4,
                PriceBeforeDiscount = 599, //ceny przed obniżką nie musisz dawać na wszystkich
            },
new DesktopPcProduct
            {
                WarantyMonths = 36,
                Cpu = new Cpu
                {
                    CoresCount=6,
                    ThreadsCount = 12,
                    FrequencyMHz = 2600,
                    Manufacturer = "Intel",
                    L3CacheMB = 12,
                    Name = "Intel Core i5-11400F",
                    Tdp = 200,
                    Architecture = "ARM",
                },
                Description = "Komputery G4M3R HERO zaprojektowaliśmy tak, by rozgrywka była płynna przy wysokich detalach. Wybraliśmy komponenty, które najlepiej do siebie pasują, dzięki czemu zachowują niskie temperatury i cichą pracę. Osiągnęliśmy to dzięki autorskim rozwiązaniom i tysiącom testów. ",
                Drives = new List<Drive>
                {
                    new Ssd
                    {
                        WriteSpeedMBs=450,
                        Interface = "sata",
                        ReadSpeedMBs = 520,
                        Manufacturer = "ADATA",
                        Tbw = 500,
                        CapacityGB = 1000,
                        Type = "SATA"
                    },
                    new Ssd
                    {
                        WriteSpeedMBs=3000,
                        Interface = "M.2 NVME 3.0",
                        ReadSpeedMBs = 3500,
                        Manufacturer = "Samsung",
                        Tbw = 500,
                        Type = "NVME"
                    }
                },
                Gpu = new Gpu
                {
                    ChipManufacturer = GpuManufacturers.Nvidia.ToString(), //jak dasz kropkę po GpuManufacturers to masz listę i dajesz tostring na koniec - dalej tak samo
                    FrequencyMHz = 1365,
                    Manufacturer = "Asus",
                    MemoryFrequencyMHz = 14000,
                    Name = "ASUS GeForce RTX 2060 DUAL EVO OC 6GB GDDR6",
                    Tdp = 150,
                    VramSizeGB = 6,
                    VramType = GpuVramTypes.GDDR6.ToString(),
                    BusWidth = 192,
                    PortsList = new(){"HDMI", "Display Port", "DVI"}
                },
                Images = new List<Image>
                {
                    new Image { Location = "https://cdn.x-kom.pl/i/setup/images/prod/big/product-new-big,,2021/11/pr_2021_11_16_14_29_58_501_02.jpg"},
                    new Image { Location = "https://cdn.x-kom.pl/i/setup/images/prod/big/product-new-big,,2021/11/pr_2021_11_16_14_29_54_486_00.jpg"},
                },
                IsPublic = true,
                IsRemoved = false,
                Name = "HERO i5-11400F 16GB RTX2060",
                Price = 5800.25m,
                Psu = new Psu {Manufacturer = "Seasonic", Power = 500},
                PriceBeforeDiscount = 6050,
                Ram = new Ram
                {
                    Manufacturer = "Crucial",
                    Name = "RAM DDR4 Ballistix 16GB",
                    FrequencyMHz = 3200,
                    LatencyCL = 16,
                    ModulesNumber = 2,
                    RamTechnology = RamTechnologies.DDR4.ToString(),
                },
                Motherboard = new Motherboard
                {
                    Manufacturer = "Asus",
                    Chipset = "Z690",
                    Name = "TUF GAMING Z690-PLUS DDR4",
                    RamType = RamTechnologies.DDR4.ToString(),
                    Socket = "1700",
                    Usb3Gen1Count = 4,
                    Usb3Gen2Count = 2,
                    UsbCCount = 2,
                    RamSlotsCount = 4,
                    Usb2Count = 0,
                },
                Cooler = new Cooler
                {
                    Manufacturer = "SilentiumPC",
                    CompatibleSockets = new List<string>{"AM4", "AM3", "AM3+", "AM2+", "FM2+", "FM1", "2066", "2011", "1700", "1200" },
                    FansCount = 1,
                    MaxTdp = 220,
                    Name = "Fortis 5 140mm",
                    CoolerType = CoolerTypes.AirCooler,
                    Sizemm = 159,
                },
                DesktopCase = new DesktopCase
                {
                    Manufacturer = "Fajnex",
                    Name = "Superx",
                    Widthmm = 216,
                    Heightmm = 488,
                    Lenghtmm = 420,
                    MaxCoolerHeightmm = 140,
                    MaxFanCount = 6,
                    MaxRadiatorSizemm = 140,
                    SupportedMoboSizes = new List<string>{MotherboardSizes.StandardAtx, MotherboardSizes.MiniATX},
                },
                Widthmm = 216,
                Heightmm = 488,
                Lenghtmm = 420,
                Manufacturer = "ASUS",
                PowerConsumption = 500,
            },


new DesktopPcProduct
            {
                WarantyMonths = 36,
                Cpu = new Cpu
                {
                    CoresCount=8,
                    ThreadsCount = 16,
                    FrequencyMHz = 3900,
                    Manufacturer = "AMD",
                    L3CacheMB = 36,
                    Name = "Ryzen 7 3800X",
                    Tdp = 200,
                    Architecture = "ARM",
                },
                Description = "Drapieżny wygląd komputera HP Pavilion Gaming pozwala podkreślić jego gamingowy charakter. W swoim wnętrzu skrywa wydajny, nowoczesny procesor oraz dedykowaną kartę graficzną. Z takim zapleczem technologicznym nie musisz już się martwić o płynność działania czy czasy reakcji. Układ chłodzenia o wysokiej sprawności pozwoli na wielogodzinne sesje wraz z Twoimi ulubionymi grami. Stawiając na HP Pavilion Gaming, stawiasz na zwycięstwo.",
                Drives = new List<Drive>
                {
                    new Ssd
                    {
                        WriteSpeedMBs=460,
                        Interface = "sata",
                        ReadSpeedMBs = 510,
                        Manufacturer = "Crucial",
                        Tbw = 500,
                        CapacityGB = 1000,
                        Type = "SATA"
                    },
                    new Ssd
                    {
                        WriteSpeedMBs=2800,
                        Interface = "M.2 NVME 3.0",
                        ReadSpeedMBs = 3300,
                        Manufacturer = "ADATA",
                        Tbw = 500,
                        Type = "NVME"
                    }
                },
                Gpu = new Gpu
                {
                    ChipManufacturer = GpuManufacturers.Nvidia.ToString(), //jak dasz kropkę po GpuManufacturers to masz listę i dajesz tostring na koniec - dalej tak samo
                    FrequencyMHz = 1830,
                    Manufacturer = "Gigabyte",
                    MemoryFrequencyMHz = 19000,
                    Name = "GeForce RTX 3070 Ti GAMING OC",
                    Tdp = 150,
                    VramSizeGB = 8,
                    VramType = GpuVramTypes.GDDR6.ToString(),
                    BusWidth = 256,
                    PortsList = new(){"HDMI", "Display Port", "DVI"}
                },
                Images = new List<Image>
                {
                    new Image { Location = "https://cdn.x-kom.pl/i/setup/images/prod/big/product-new-big,,2021/11/pr_2021_11_26_11_22_58_503_00.jpg"},
                    new Image { Location = "https://cdn.x-kom.pl/i/setup/images/prod/big/product-new-big,,2021/11/pr_2021_11_26_11_23_2_721_02.jpg"},
                },
                IsPublic = true,
                IsRemoved = false,
                Name = "HP Pavilion Gaming Super",
                Price = 3499,
                Psu = new Psu {Manufacturer = "SilentiumPC", Power = 310},
                PriceBeforeDiscount = 3499,
                Ram = new Ram
                {
                    Manufacturer = "Patriot",
                    Name = "16GB (2x8GB) 3600MHz CL17 Viper Steel",
                    FrequencyMHz = 3600,
                    LatencyCL = 17,
                    ModulesNumber = 2,
                    RamTechnology = RamTechnologies.DDR4.ToString(),
                },
                Motherboard = new Motherboard
                {
                    Manufacturer = "MSI",
                    Chipset = "B550",
                    Name = "MAG B550 TOMAHAWK",
                    RamType = RamTechnologies.DDR4.ToString(),
                    Socket = "AM4",
                    Usb3Gen1Count = 1,
                    Usb3Gen2Count = 1,
                    UsbCCount = 1,
                    RamSlotsCount = 4,
                    Usb2Count = 2,
                },
                Cooler = new Cooler
                {
                    Manufacturer = "SilentiumPC",
                    CompatibleSockets = new List<string>{"AM4", "AM3", "AM3+", "AM2+", "FM2+", "FM1", "2066", "2011", "1700", "1200" },
                    FansCount = 1,
                    MaxTdp = 220,
                    Name = "Fortis 5 140mm",
                    CoolerType = CoolerTypes.AirCooler,
                    Sizemm = 159,
                },
                DesktopCase = new DesktopCase
                {
                    Manufacturer = "HP",
                    Name = "Super",
                    Widthmm = 155,
                    Heightmm = 340,
                    Lenghtmm = 325,
                    MaxCoolerHeightmm = 140,
                    MaxFanCount = 4,
                    MaxRadiatorSizemm = 140,
                    SupportedMoboSizes = new List<string>{MotherboardSizes.StandardAtx, MotherboardSizes.MiniATX},
                },
                Widthmm = 340,
                Heightmm = 488,
                Lenghtmm = 325,
                Manufacturer = "HP",
                PowerConsumption = 300,
            },

new DesktopPcProduct
            {
                WarantyMonths = 36,
                Cpu = new Cpu
                {
                    CoresCount=4,
                    ThreadsCount = 8,
                    FrequencyMHz = 2500,
                    Manufacturer = "AMD",
                    L3CacheMB = 14,
                    Name = "Ryzen 6 2600",
                    Tdp = 200,
                    Architecture = "ARM",
                },
                Description = "Drapieżny wygląd komputera Acer Nitro 50 pozwala podkreślić jego gamingowy charakter. W swoim wnętrzu skrywa wydajny, nowoczesny procesor oraz dedykowaną kartę graficzną. Z takim zapleczem technologicznym nie musisz już się martwić o płynność działania czy czasy reakcji. Układ chłodzenia o wysokiej sprawności pozwoli na wielogodzinne sesje wraz z Twoimi ulubionymi grami. Stawiając na Acer Nitro 50, stawiasz na zwycięstwo.",
                Drives = new List<Drive>
                {
                    new Ssd
                    {
                        WriteSpeedMBs=470,
                        Interface = "sata",
                        ReadSpeedMBs = 520,
                        Manufacturer = "Corsair",
                        Tbw = 500,
                        CapacityGB = 2000,
                        Type = "SATA"
                    },
                    new Ssd
                    {
                        WriteSpeedMBs=2800,
                        Interface = "M.2 NVME 3.0",
                        ReadSpeedMBs = 3300,
                        Manufacturer = "ADATA",
                        Tbw = 500,
                        Type = "NVME"
                    }
                },
                Gpu = new Gpu
                {
                    ChipManufacturer = GpuManufacturers.Nvidia.ToString(), //jak dasz kropkę po GpuManufacturers to masz listę i dajesz tostring na koniec - dalej tak samo
                    FrequencyMHz = 1830,
                    Manufacturer = "Gigabyte",
                    MemoryFrequencyMHz = 19000,
                    Name = "GeForce RTX 3070 Ti GAMING OC",
                    Tdp = 150,
                    VramSizeGB = 8,
                    VramType = GpuVramTypes.GDDR6.ToString(),
                    BusWidth = 256,
                    PortsList = new(){"HDMI", "Display Port", "DVI"}
                },
                Images = new List<Image>
                {
                    new Image { Location = "https://cdn.x-kom.pl/i/setup/images/prod/big/product-new-big,,2021/7/pr_2021_7_2_12_8_13_628_00.jpg"},
                    new Image { Location = "https://cdn.x-kom.pl/i/setup/images/prod/big/product-new-big,,2021/7/pr_2021_7_2_12_8_16_96_01.jpg"},
                },
                IsPublic = true,
                IsRemoved = false,
                Name = "Acer Nitro 50",
                Price = 4399,
                Psu = new Psu {Manufacturer = "SilentiumPC", Power = 500},
                PriceBeforeDiscount = 4399,
                Ram = new Ram
                {
                    Manufacturer = "Corsair",
                    Name = "16GB (2x8GB) 3400MHz CL17 Boarding",
                    FrequencyMHz = 3400,
                    LatencyCL = 17,
                    ModulesNumber = 2,
                    RamTechnology = RamTechnologies.DDR4.ToString(),
                },
                Motherboard = new Motherboard
                {
                    Manufacturer = "MSI",
                    Chipset = "B450",
                    Name = "MAG B550 TOMAHAWK",
                    RamType = RamTechnologies.DDR4.ToString(),
                    Socket = "AM4",
                    Usb3Gen1Count = 1,
                    Usb3Gen2Count = 1,
                    UsbCCount = 1,
                    RamSlotsCount = 4,
                    Usb2Count = 2,
                },
                Cooler = new Cooler
                {
                    Manufacturer = "SilentiumPC",
                    CompatibleSockets = new List<string>{"AM4", "AM3", "AM3+", "AM2+", "FM2+", "FM1", "2066", "2011", "1700", "1200" },
                    FansCount = 1,
                    MaxTdp = 220,
                    Name = "Air 5 140mm",
                    CoolerType = CoolerTypes.AirCooler,
                    Sizemm = 159,
                },
                DesktopCase = new DesktopCase
                {
                    Manufacturer = "Acer",
                    Name = "Gyt",
                    Widthmm = 175,
                    Heightmm = 361,
                    Lenghtmm = 380,
                    MaxCoolerHeightmm = 140,
                    MaxFanCount = 4,
                    MaxRadiatorSizemm = 140,
                    SupportedMoboSizes = new List<string>{MotherboardSizes.StandardAtx, MotherboardSizes.MiniATX},
                },
                Widthmm = 175,
                Heightmm = 361,
                Lenghtmm = 380,
                Manufacturer = "Acer",
                PowerConsumption = 500,
            },
 new ComputerMouseProduct
            {
                Weightg = 85,
                Widthmm = 62,
                IsWireless = false,
                Color = "Czarny",
                Heightmm = 38,
                Lenghtmm = 116,
                Manufacturer = "Logitech",
                Sensor = "Pixart 3061",
                SensorType = "Optyczny",
                Interface = "USB",
                Name = "G102 LIGHTSYNC czarna",
                PollingRateHz = 5000,
                Price = 119,
                WarantyMonths = 24,
                Description = "G102 Lightsync to przewodowa mysz z klasyczną 6-przyciskową, wygodną konstrukcję w kolorze czarnym. Urządzenie wyposażono w czujnik przeznaczony do gier. Co więcej, mysz posiada podświetlenie Lightsync, które nie tylko sprawi, że gra będzie bardziej wciągająca, ale również podświetli Twoje biurko.",
                Images = new(){ new Image{Location = "https://cdn.x-kom.pl/i/setup/images/prod/big/product-new-big,,2020/6/pr_2020_6_24_11_28_3_28_00.jpg"} },
                IsPublic = true,
                MaxDpi = 8000,
            },
 new ComputerMouseProduct
            {
                Weightg = 77,
                Widthmm = 67,
                IsWireless = false,
                Color = "Czarny",
                Heightmm = 38,
                Lenghtmm = 121,
                Manufacturer = "SteelSeries",
                Sensor = "Pixart 3061",
                SensorType = "Optyczny",
                Interface = "USB",
                Name = "Rival 3",
                PollingRateHz = 5000,
                Price = 169,
                WarantyMonths = 24,
                Description = "Najnowsza mysz Rival 3 od firmy SteelSeries to następca legendarnej myszki Rival 110. Oznacza to, że jest jeszcze lepsza od swojej genialnej poprzedniczki. Mysz Rival 3 została wykonana z bardzo wytrzymałych materiałów, a jednocześnie jej konstrukcja jest lekka, komfortowa i ergonomiczna.",
                Images = new(){ new Image{Location = "https://cdn.x-kom.pl/i/setup/images/prod/big/product-new-big,,2020/1/pr_2020_1_20_9_50_4_411_00.jpg"} },
                IsPublic = true,
                MaxDpi = 8500,
            },
 new ComputerMouseProduct
            {
                Weightg = 135,
                Widthmm = 71,
                IsWireless = true,
                Color = "Czarny",
                Heightmm = 42,
                Lenghtmm = 109,
                Manufacturer = "Logitech",
                Sensor = "Pixart 3061",
                SensorType = "Laser",
                Interface = "USB",
                Name = "M705 Marathon",
                PollingRateHz = 5000,
                Price = 139,
                WarantyMonths = 36,
                Description = "Zapomnij o niewygodach, kosztach i trudach dla środowiska naturalnego związanych z częstymi zmianami baterii. Trzy lata zasilania. Wystarczy podłączyć niewielki odbiornik Logitech Unifying i zapomnieć o nim. Możesz dodać nawet więcej urządzeń. Podłącz. Zapomnij. Dodaj kolejne. Superszybkie przewijanie. Po jednym ruchu kółka już nigdy nie wrócisz do normalnego kółka przewijania.",
                Images = new(){ new Image{Location = "https://cdn.x-kom.pl/i/setup/images/prod/big/product-new-big,,2020/4/pr_2020_4_22_8_41_20_378_00.jpg"} },
                IsPublic = true,
                MaxDpi = 1000,
            },
 new MotherboardProduct
            {
                Chipset = "B560",
                Description = "Korzystaj z wydajności procesorów Intel Core 11. i 10. generacji z płytą główną Gigabyte B560M DS3H V2, należącej do słynącej z niezawodności serii Ultra Durable. Zapewnia ona wsparcie dla PCIe 4.0 w formie slotu dla kart graficznych oraz łączności z szybkimi dyskami SSD M.2. Z kolei trzy wyjścia wideo: DVI, HDMI i DisplayPort pozwolą Ci skorzystać z wbudowanych w CPU układów graficznych.",
                Images = new(){new Image { Location="https://www.gigabyte.pl/products/upload/products/8635/19f41a72_6.png"},
                               new Image { Location="https://www.gigabyte.pl/products/upload/products/8635/5f4033a1_6.png"}},
                M2SlotsCount = 2,
                Manufacturer = "Gigabyte",
                PcieX16SlotsCount = 1,
                Price = 399,
                RamSlotsCount = 4,
                RamType = RamTechnologies.DDR4.ToString(),
                SataConnectorsCount = 6,
                Name = "B560M DS3H V2",
                Size = MotherboardSizes.FlexATX,
                Socket = "1200",
                WarantyMonths = 24,
                IsPublic = true,
                Usb2Count = 2,
                Usb3Gen1Count = 1,
                Usb3Gen2Count = 0,
                UsbCCount = 0,
            },
new MotherboardProduct
            {
                Chipset = "Z690",
                Description = "Połącz wydajność procesorów Intel Core 12. generacji z kultową, gamingową serią TUF. Płyta główna ASUS TUF GAMING Z690-PLUS DDR4 z podstawką LGA 1700 zapewnia wsparcie dla powszechnej pamięci DDR4 o taktowaniu nawet 5333 MHz (OC), a także oferuje slot PCIe 5.0 x16 o przepustowości 128 Gb/s, by w pełni wykorzystać możliwości nowych kart graficznych.",
                Images = new(){new Image { Location="https://dlcdnwebimgs.asus.com/gain/af81f1ac-908d-4bb3-8638-45ec6016facb/w692"}},
                M2SlotsCount = 4,
                Manufacturer = "ASUS",
                PcieX16SlotsCount = 2,
                Price = 1299,
                RamSlotsCount = 4,
                RamType = RamTechnologies.DDR4.ToString(),
                SataConnectorsCount = 4,
                Name = "TUF GAMING Z690-PLUS DDR4",
                Size = MotherboardSizes.FlexATX,
                Socket = "1700",
                WarantyMonths = 36,
                IsPublic = true,
                Usb2Count = 0,
                Usb3Gen1Count = 4,
                Usb3Gen2Count = 2,
                UsbCCount = 2,
            },
new MotherboardProduct
            {
                Chipset = "B550",
                Description = "Seria płyt głównych MAG to symbol trwałości i niezawodności, skoncentrowana na zapewnianiu jak najlepszych wrażeń płynących z gier. Dlatego bojowa płyta główna MSI MAG B550 TOMAHAWK pozwoli Ci wykorzystać moc procesorów AMD Ryzen 3. i kolejnej generacji. Ponadto możesz podłączać moduły RAM podkręcone do taktowania nawet 4866 MHz (OC) oraz szybkie dyski SSD M.2 na interfejsie PCIe 4.0 x4.",
                Images = new(){new Image { Location="https://asset.msi.com/resize/image/global/product/product_160938336626fdb6ca106517e76e29ed491af5e8c7.png62405b38c58fe0f07fcef2367d8a9ba1/1024.png"},
                               new Image { Location="https://asset.msi.com/resize/image/global/product/product_5_20200520165805_5ec4f11d89e51.png62405b38c58fe0f07fcef2367d8a9ba1/1024.png"}},
                M2SlotsCount = 2,
                Manufacturer = "MSI",
                PcieX16SlotsCount = 2,
                Price = 829,
                RamSlotsCount = 4,
                RamType = RamTechnologies.DDR4.ToString(),
                SataConnectorsCount = 6,
                Name = "MAG B550 TOMAHAWK",
                Size = MotherboardSizes.FlexATX,
                Socket = "AM4",
                WarantyMonths = 36,
                IsPublic = true,
                Usb2Count = 2,
                Usb3Gen1Count = 1,
                Usb3Gen2Count = 1,
                UsbCCount = 1,
            },
new MonitorProduct
            {
                BrightnessCdm = 250,
                Manufacturer = "Acer",
                Name = "Nitro VG240YBMIIX czarny",
                PanelSizeInch = 23,
                Ports = new List<string>{ GpuPorts.VGA.ToString(), GpuPorts.DispalyPort.ToString()},
                Contrast = 1000,
                PanelType = "LED,IPS",
                RefreshRateHz = 75,
                ResponseTimems =  1,
                Price = 599,
                ResolutionXpx = 1920,
                ResolutionYpx = 1080,
                SrgbColorSpacePerc = 99,
                WarantyMonths= 24,
                Widthmm = 540,
                AspectRatio = "16:9",
                Description = "Zdominuj pole walki z monitorem dla graczy Acer Nitro VG240YBMIIX. Błyskawiczny czas reakcji matrycy da Ci dodatkowe ułamki sekundy w szybkich starciach. Płynność obrazu przełoży się na zwiększoną precyzję celowania, dzięki czemu zyskasz dodatkową przewagę nad przeciwnikiem. Co więcej, dynamika obrazu oraz rewelacyjne barwy sprawią, że jeszcze bardziej zagłębisz się w rozgrywce.",
                Heightmm = 409,
                IsPublic = true,
                Lenghtmm = 240,
                Images = new(){ new Image{Location = "https://cdn.x-kom.pl/i/setup/images/prod/big/product-new-big,,2018/7/pr_2018_7_23_8_51_26_666_06.jpg"} },
            },
new MonitorProduct
            {
                BrightnessCdm = 300,
                Manufacturer = "LG",
                Name = "24GN600-B",
                PanelSizeInch = 23,
                Ports = new List<string>{ GpuPorts.VGA.ToString(), GpuPorts.DispalyPort.ToString()},
                Contrast = 1000,
                PanelType = "IPS",
                RefreshRateHz = 144,
                ResponseTimems =  1,
                Price = 699,
                ResolutionXpx = 1920,
                ResolutionYpx = 1080,
                SrgbColorSpacePerc = 97,
                WarantyMonths= 24,
                Widthmm = 541,
                AspectRatio = "16:9",
                Description = "Jeżeli w ciągu 45 dni od daty zakupu monitora LG wystąpi wada matrycy, polegająca na ciągłym świeceniu się lub nie świeceniu któregokolwiek subpiksela, producent gwarantuje wymianę monitora na nowy. Promocją objęty jest każdy monitor LG zakupiony w okresie trwania promocji (do 31 grudnia 2022 roku) i posiadający co najmniej jedno złącze cyfrowe: HDMI / DVI / Display Port.",
                Heightmm = 409,
                IsPublic = true,
                Lenghtmm = 181,
                Images = new(){ new Image{Location = "https://cdn.x-kom.pl/i/setup/images/prod/big/product-new-big,,2021/10/pr_2021_10_20_11_15_42_456_00.jpg"} },
            },
new MonitorProduct
            {
                BrightnessCdm = 250,
                Manufacturer = "Samsung",
                Name = "Odyssey F24G35TFWUX",
                PanelSizeInch = 23,
                Ports = new List<string>{ GpuPorts.VGA.ToString(), GpuPorts.DispalyPort.ToString()},
                Contrast = 4000,
                PanelType = "IPS",
                RefreshRateHz = 144,
                ResponseTimems =  1,
                Price = 849,
                ResolutionXpx = 1920,
                ResolutionYpx = 1080,
                SrgbColorSpacePerc = 99,
                WarantyMonths= 24,
                Widthmm = 541,
                AspectRatio = "16:9",
                Description = "Zachwycający design oraz niezwykle bogata funkcjonalność – to czyni z monitora Samsung Odyssey F24G35TFWUX narzędzie, dzięki któremu odkryjesz gaming na nowo. Solidna konstrukcja połączona z panelem VA Full HD oferuje najlepsze doznania z gry w każdym calu. Bogate kolory, najdrobniejsze szczegóły i niezwykle szybki czas reakcji to cechy, dzięki którym odniesiesz sukces na wirtualnych polach bitwy. Poznaj gamingowy monitor Samsung Odyssey F24G35TFWUX.",
                Heightmm = 364,
                IsPublic = true,
                Lenghtmm = 264,
                Images = new(){ new Image{Location = "https://cdn.x-kom.pl/i/setup/images/prod/big/product-new-big,,2021/1/pr_2021_1_15_11_15_5_574_00.jpg"} },
            },
new LaptopProduct
            {
                WarantyMonths = 24,
                BatteryCapacitymAh = 4700,
                Cpu = new Cpu
                {
                    CoresCount=8,
                    ThreadsCount = 16,
                    FrequencyMHz = 2300,
                    Manufacturer = "Intel",
                    L3CacheMB = 24,
                    Name = "Core i7-11800H",
                    Tdp = 150,
                    Architecture = "x64"
                },
                Drives = new List<Drive>
                {
                    new Ssd
                    {
                        WriteSpeedMBs=512,
                        Interface = "PCIe",
                        ReadSpeedMBs = 500,
                        Manufacturer = "WD",
                        Tbw = 480,
                        CapacityGB = 512,
                        Type = "M.2"
                    },
                },
                DisplaySize = 17.3m, //15.6 14 17.3
                Description = "MSI GF76 Katana nie powstał w ogniu kuźni jak słynny japoński miecz, ale nadal wykonano go z takim samym kunsztem i precyzją. Z takim orężem możesz śmiało stanąć do wirtualnych bitew, by zdobywać szczyty tabel rankingowych. Nowoczesny procesor i7 11. generacji, karta graficzna RTX 3060 i ekran ze 144 Hz odświeżaniem staną się Twoimi wiernymi kompanami. Razem z nimi wyjdziesz zwycięsko z każdej wymagającej potyczki – z tarczą, a nie na tarczy.",
                Gpu = new Gpu
                {
                    ChipManufacturer = GpuManufacturers.Nvidia.ToString(),
                    FrequencyMHz = 1807,
                    Manufacturer = "MSI",
                    MemoryFrequencyMHz = 15000,
                    Name = "RTX 3060",
                    Tdp = 150,
                    VramSizeGB = 12,
                    VramType = GpuVramTypes.GDDR6.ToString(),
                    BusWidth = 192,
                    PortsList = new() {"HDMI", "Display Port" }
                },
                Manufacturer = "MSI",
                Images = new List<Image>
                {
                    new Image { Location = "https://cdn.x-kom.pl/i/setup/images/prod/big/product-new-big,,2021/9/pr_2021_9_10_13_23_7_145_04.jpg"},
                    new Image { Location = "https://cdn.x-kom.pl/i/setup/images/prod/big/product-new-big,,2021/9/pr_2021_9_10_13_23_3_852_02.jpg"},
                },
                 Name = "GF76",
                 Motherboard = new Motherboard()
                 {
                     Chipset = "B450",
                     RamSlotsCount = 4,
                     Socket = "AM4",
                     RamType = RamTechnologies.DDR4.ToString(),
                     Manufacturer = "MSI",
                     Name = "Tomahawk",
                     Usb2Count = 2,
                     Usb3Gen1Count = 2,
                     Usb3Gen2Count = 2,
                     UsbCCount = 1,
                 },
                 Price = 5799,
                 Psu = new Psu(){ Manufacturer="MSI", Power = 120},
                 Ram = new Ram
                {
                    Manufacturer = "Corsair",
                    Name = "LPX 100",
                    FrequencyMHz = 3000,
                    LatencyCL = 17,
                    ModulesNumber = 2,
                    RamTechnology = RamTechnologies.DDR4.ToString()
                },
                 Widthmm = 398,
                 BatteryLifeIdleMin = 300,
                 BatteryLifeUnderLoadMin = 120,
                 Heightmm = 25,
                 IsPublic = true,
                 Lenghtmm = 273,
            },

new LaptopProduct
            {
                WarantyMonths = 24,
                BatteryCapacitymAh = 4940,
                Cpu = new Cpu
                {
                    CoresCount=4,
                    ThreadsCount = 8,
                    FrequencyMHz = 3300,
                    Manufacturer = "Intel",
                    L3CacheMB = 12,
                    Name = "Core i7-11370H",
                    Tdp = 150,
                    Architecture = "x64"
                },
                Drives = new List<Drive>
                {
                    new Ssd
                    {
                        WriteSpeedMBs=512,
                        Interface = "PCIe",
                        ReadSpeedMBs = 500,
                        Manufacturer = "WD",
                        Tbw = 480,
                        CapacityGB = 512,
                        Type = "M.2"
                    },
                },
                DisplaySize = 15.6m, //15.6 14 17.3
                Description = "Gotuj się do walki. Gamingowy laptop ASUS TUF Dash F15 wprowadzi Cię na pola wirtualnych bitew, oddając do dyspozycji arsenał, który poprowadzi Cię do niezliczonych zwycięstw. Wyposażony został w wyselekcjonowane, ultrawydajne komponenty - nowoczesny procesor oraz dedykowaną kartę graficzną. Z takim zapleczem technologicznym Twoi rywale mogą co najwyżej przygotowywać się do odwrotu.",
                Gpu = new Gpu
                {
                    ChipManufacturer = GpuManufacturers.Nvidia.ToString(),
                    FrequencyMHz = 1807,
                    Manufacturer = "ASUS",
                    MemoryFrequencyMHz = 15000,
                    Name = "RTX 3060",
                    Tdp = 150,
                    VramSizeGB = 12,
                    VramType = GpuVramTypes.GDDR6.ToString(),
                    BusWidth = 192,
                    PortsList = new() {"HDMI", "Display Port" }
                },
                Manufacturer = "MSI",
                Images = new List<Image>
                {
                    new Image { Location = "https://cdn.x-kom.pl/i/setup/images/prod/big/product-new-big,,2021/1/pr_2021_1_8_18_42_55_897_05.jpg"},
                    new Image { Location = "https://cdn.x-kom.pl/i/setup/images/prod/big/product-new-big,,2021/1/pr_2021_1_8_18_42_52_351_03.jpg"},
                },
                 Name = "TUF Dash F15",
                 Motherboard = new Motherboard()
                 {
                     Chipset = "B550",
                     RamSlotsCount = 4,
                     Socket = "AM4",
                     RamType = RamTechnologies.DDR4.ToString(),
                     Manufacturer = "ASUS",
                     Name = "SUS",
                     Usb2Count = 1,
                     Usb3Gen1Count = 2,
                     Usb3Gen2Count = 2,
                     UsbCCount = 2,
                 },
                 Price = 4999,
                 Psu = new Psu(){ Manufacturer="ASUS", Power = 120},
                 Ram = new Ram
                {
                    Manufacturer = "Corsair",
                    Name = "LPX 100",
                    FrequencyMHz = 3000,
                    LatencyCL = 17,
                    ModulesNumber = 2,
                    RamTechnology = RamTechnologies.DDR4.ToString()
                },
                 Widthmm = 360,
                 BatteryLifeIdleMin = 280,
                 BatteryLifeUnderLoadMin = 110,
                 Heightmm = 20,
                 IsPublic = true,
                 Lenghtmm = 252,
            },

new LaptopProduct
            {
                WarantyMonths = 24,
                BatteryCapacitymAh = 5210,
                Cpu = new Cpu
                {
                    CoresCount=8,
                    ThreadsCount = 16,
                    FrequencyMHz = 3200,
                    Manufacturer = "AMD",
                    L3CacheMB = 20,
                    Name = "Ryzen 7 5800H",
                    Tdp = 150,
                    Architecture = "x64"
                },
                Drives = new List<Drive>
                {
                    new Ssd
                    {
                        WriteSpeedMBs=512,
                        Interface = "PCIe",
                        ReadSpeedMBs = 500,
                        Manufacturer = "WD",
                        Tbw = 480,
                        CapacityGB = 1000,
                        Type = "M.2"
                    },
                },
                DisplaySize = 17.3m, //15.6 14 17.3
                Description = "Lenovo Legion 5-17 powstał w jednym celu – aby wprowadzić Cię na najwyższe szczeble gamingowej drabiny. Wewnątrz laptopa na Twoje e-sportowe sukcesy pracują m.in.: ultrawydajny procesor oraz karta graficzna GeForce RTX. Efekty ich pracy ujrzysz w rozdzielczości Full HD na wysokiej jakości ekranie. Podejmij wyzwanie i dołącz do wirtualnych pojedynków z laptopem Lenovo Legion 5-17.",
                Gpu = new Gpu
                {
                    ChipManufacturer = GpuManufacturers.Nvidia.ToString(),
                    FrequencyMHz = 1807,
                    Manufacturer = "ASUS",
                    MemoryFrequencyMHz = 15000,
                    Name = "RTX 3060",
                    Tdp = 150,
                    VramSizeGB = 12,
                    VramType = GpuVramTypes.GDDR6.ToString(),
                    BusWidth = 192,
                    PortsList = new() {"HDMI", "Display Port" }
                },
                Manufacturer = "MSI",
                Images = new List<Image>
                {
                    new Image { Location = "https://cdn.x-kom.pl/i/setup/images/prod/big/product-new-big,,2021/8/pr_2021_8_13_9_29_7_546_00.jpg"},
                    new Image { Location = "https://cdn.x-kom.pl/i/setup/images/prod/big/product-new-big,,2021/8/pr_2021_8_13_9_29_13_217_03.jpg"},
                },
                 Name = "Legion 5-17`",
                 Motherboard = new Motherboard()
                 {
                     Chipset = "B650",
                     RamSlotsCount = 4,
                     Socket = "AM4",
                     RamType = RamTechnologies.DDR4.ToString(),
                     Manufacturer = "Lenovo",
                     Name = "Predator",
                     Usb2Count = 1,
                     Usb3Gen1Count = 2,
                     Usb3Gen2Count = 2,
                     UsbCCount = 2,
                 },
                 Price = 4999,
                 Psu = new Psu(){ Manufacturer="Silentium", Power = 150},
                 Ram = new Ram
                {
                    Manufacturer = "Patriot",
                    Name = "Viper 100",
                    FrequencyMHz = 3000,
                    LatencyCL = 17,
                    ModulesNumber = 2,
                    RamTechnology = RamTechnologies.DDR4.ToString()
                },
                 Widthmm = 399,
                 BatteryLifeIdleMin = 350,
                 BatteryLifeUnderLoadMin = 180,
                 Heightmm = 26,
                 IsPublic = true,
                 Lenghtmm = 290,
            },
        };
        public async Task<Product?> GetProductByIdAsync(string id, bool isAdmin = false)
        {
            if(isAdmin)
                return await productsData.GetProductAsync(id);
            return await productsData.GetPublicProductAsync(id);
        }
        public async Task<List<Product>> GetAllProductsAsync()
        {
            //foreach (var item in Products)
            //{
            //    await productsData.AddProductAsync(item);
            //}
            return await productsData.GetAllProductsAsync();
        }
        public async Task<List<Product>> GetHighlightedProductsAsync()
        {
            return (await productsData.GetAllPublicProductsAsync()).Where(p => p.IsHiglighted).ToList();
        }
        public async Task<ProductsResponse> GetProductsByCategoryAsync(string category, int pageNumber = 1, ProductSortFilterOptions? sortFilterOptions = null, bool isAdmin = false)
        {
            category = category.ToLower();
            List<Product>? prod = new();
            if (isAdmin)
                prod = await productsData.GetAllProductsAsync();
            else
                prod = await productsData.GetAllPublicProductsAsync();
            List<Product>? products = prod.Where(x => x.Category?.ToLower().Equals(category) ?? false).ToList();
            var manfucturers = GetManufacturers(products);
            if (sortFilterOptions != null)
                products = productHelper.SortFilterProducts(products, sortFilterOptions).ToList();            
            var pr = GetProductsResponse(products, pageNumber);
            pr.Manufacturers = manfucturers;
            return pr;
        }
        public async Task<ProductsResponse> FindProductsByTextAsync(string text, int pageNumber = 1, ProductSortFilterOptions? sortFilterOptions = null, bool isAdmin = false)
        {
            List<Product>? foundProducts = await FindProducts(text, isAdmin);
            var manfucturers = GetManufacturers(foundProducts);
            if (sortFilterOptions != null)
                foundProducts = productHelper.SortFilterProducts(foundProducts, sortFilterOptions).ToList();            
            var pr = GetProductsResponse(foundProducts, pageNumber);
            pr.Manufacturers = manfucturers;
            return pr;
        }
        public async Task<List<string>> GetProductsSuggestionsByTextAsync(string text)
        {
            text = text.ToLower();
            List<string> suggestions = new();
            List<Product> products = await FindProducts(text);
            if (products == null || products.Count == 0)
            {
                return suggestions;
            }

            suggestions.AddRange(products
                .Select(p => p.Name)?
                .Where(s => s != null && s.Contains(text, StringComparison.OrdinalIgnoreCase))
                .ToList() ?? new List<string>());

            suggestions.AddRange(products
                .Select(p => p.Manufacturer)?
                .Where(s => s != null && s.Contains(text, StringComparison.OrdinalIgnoreCase))
                .ToList() ?? new List<string>());

            return suggestions.Distinct().ToList();
        }
        private async Task<List<Product>> FindProducts(string text, bool isAdmin = false)
        {
            text = text.ToLower();
            List<string>? words = text.Split(' ').Distinct().ToList();
            List<Product> products = new();
            List<Product>? p = new();
            if (isAdmin)
                p = await productsData.GetAllProductsAsync();
            else
                p = await productsData.GetAllPublicProductsAsync();
            words.ForEach(word => products.AddRange(p
                .Where(x => (x.Manufacturer != null && x.Manufacturer.Contains(word, StringComparison.OrdinalIgnoreCase)) ||
                            (x.Name != null && x.Name.Contains(word, StringComparison.OrdinalIgnoreCase)) ||
                            (x.Description != null && x.Description.Contains(word, StringComparison.OrdinalIgnoreCase)))));
            return products.Distinct().ToList();
        }
        private ProductsResponse GetProductsResponse(List<Product> products, int pageNumber)
        {
            if (pageNumber < 1)
                return new ProductsResponse() { };
            int maxProductsOnPage = 12;
            int pageCount = 0;
            if (products != null && products.Count > 0)
            {
                float x = products.Count / (float)maxProductsOnPage;
                pageCount = (int)Math.Ceiling(x);
                products = products.Skip((pageNumber - 1) * maxProductsOnPage).Take(maxProductsOnPage).ToList();
            }
            else if (products == null)
            {
                products = new List<Product>();
            }
            return new ProductsResponse()
            {
                Products = products,
                CurrentPage = pageNumber,
                PagesCount = pageCount
            };
        }
        public async Task<List<Product>> GetProductsByIdListAsync(List<string> idList, bool isAdmin = false)
        {
            List<Product> products = new();
            foreach (var item in idList)
            {
                var prod = await GetProductByIdAsync(item, isAdmin);
                if (prod != null)
                    products.Add(prod);
            }
            return products;
        }
        public async Task<SimpleServiceResponse> AddProductAsync(Product product)
        {
            if (product is null)
                return new SimpleServiceResponse() { Message = "Produkt nie może mieć wartości null", Success = false };
            try
            {
                await productsData.AddProductAsync(product);
                return new SimpleServiceResponse() { Success = true };
            }
            catch (MongoException ex)
            {
                return new SimpleServiceResponse() { Success = false, Message = ex.Message };
            }
        }
        public async Task<SimpleServiceResponse> AddCommentToProductAsync(Comment comment, string productId)
        {
            if (comment is null || string.IsNullOrEmpty(productId))
                return new SimpleServiceResponse() { Message = "Komentarz lub id produktu nie może mieć wartości null", Success = false };
            var product = await productsData.GetProductAsync(productId);
            if (product == null)
                return new SimpleServiceResponse() { Message = "Produkt nie może mieć wartości null", Success = false };
            product.Comments.Add(comment);
            await UpdateProductAsync(product);
            return new SimpleServiceResponse() { Success = true };
        }
        public async Task<SimpleServiceResponse> UpdateProductAsync(Product product)
        {
            if (product is null)
                return new SimpleServiceResponse() { Message = "Produkt nie może mieć wartości null", Success = false };
            try
            {
                await productsData.UpdateProductAsync(product);
                return new SimpleServiceResponse() { Success = true };
            }
            catch (MongoException ex)
            {
                return new SimpleServiceResponse() { Success = false, Message = ex.Message };
            }
        }
        public HashSet<string> GetManufacturers(List<Product> products)
        {
            return products.Select(p => p.Manufacturer).OrderBy(x => x).ToHashSet();
        }
    }
}