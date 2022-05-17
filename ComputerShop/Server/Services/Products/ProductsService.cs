using ComputerShop.Server.DataAccess;
using ComputerShop.Shared.Models;
using ComputerShop.Shared.Models.Components;
using ComputerShop.Shared.Models.Products;

namespace ComputerShop.Server.Services.Products
{
    public class ProductsService : IProductsService
    {
        //tmp check if load is needed
        public List<Product> Products { get; set; } = new List<Product>
        {
            new DesktopPcProduct
            {
                WarantyMonths = 24,
                Cpu = new Cpu
                {
                    CoresCount=8,
                    ThreadsCount = 16,
                    FrequencyMHz = 2500,
                    Manufacturer = "AMD",
                    L3CacheMB = 25,
                    Name = "Ryzen 7 2700",
                    Tdp = 150,
                },
                Description = " Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla quis orci semper, convallis purus ut, hendrerit enim. Proin suscipit, nunc sit amet sollicitudin gravida, tellus mi feugiat dolor, porta dignissim ante felis eu quam. In tincidunt sollicitudin eros vel vulputate. Fusce augue massa, interdum ut consectetur feugiat, laoreet ut erat. Mauris eleifend bibendum rutrum. Ut vel bibendum sapien, ut facilisis lorem. Quisque rhoncus consequat arcu nec vestibulum. Nullam sit amet congue arcu. Vestibulum sit amet luctus lectus. Duis maximus elit a est tristique, eu eleifend leo viverra. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Nam ut euismod felis. Nullam ullamcorper, dolor et ullamcorper dictum, sem augue ultricies lorem, at pretium quam dolor at nisl. Phasellus sit amet ex efficitur, vehicula arcu vel, efficitur eros. Aliquam quis tortor sed mi egestas vehicula. Mauris pellentesque lorem feugiat quam faucibus, at dapibus nulla sagittis. Phasellus nec suscipit mauris, sed gravida orci. In ultrices urna est, ac dapibus mauris dignissim quis. Cras dui odio, elementum quis fringilla in, rutrum nec nisi. Curabitur in lobortis nunc, ut ornare lectus. Etiam eget mollis metus. Aliquam placerat turpis eu dictum ultricies. Integer interdum dictum tellus non consequat. Aliquam erat quam, commodo non ipsum id, molestie pellentesque enim. Etiam egestas, diam ut accumsan blandit, lorem nisi imperdiet lacus, eu semper nunc lacus id arcu. Donec sit amet cursus mi, nec mattis purus. Sed ut tellus nec purus faucibus molestie ut id turpis. Morbi ut metus suscipit sem pharetra iaculis eu eu nisl. Phasellus dignissim auctor purus nec placerat. Proin ullamcorper, odio suscipit fringilla dictum, arcu elit luctus nibh, in gravida dui metus nec enim. Mauris auctor congue malesuada. Cras gravida sit amet diam mollis malesuada. Curabitur sollicitudin magna scelerisque lectus malesuada, quis pulvinar diam pharetra. Donec eget eros vitae nisl suscipit iaculis. Nunc et volutpat quam, at ullamcorper nibh. Nulla facilisi. Donec eget aliquam lorem, sit amet placerat mauris. Sed consectetur, felis nec blandit laoreet, tortor libero posuere purus, sed auctor risus lectus non tortor. Pellentesque nec arcu vehicula, finibus leo ut, venenatis purus. Vivamus vitae pretium enim. Duis eu elementum augue. Vestibulum ultrices massa non risus pulvinar mollis. Vestibulum non elit enim. Quisque ac odio sem. Nulla facilisi. In nec mi sit amet leo semper ornare sed at ante. Duis non quam finibus, consequat ante quis, finibus magna. Maecenas vestibulum elit orci, sit amet congue sem posuere nec. Integer et ultricies lectus. Donec id pharetra tortor, quis feugiat risus. Nulla consectetur mattis justo. Nulla consectetur hendrerit risus, sed pretium urna commodo sit amet. Nullam ut neque finibus, bibendum nunc eget, placerat libero. Nam at varius ex, ac pretium felis. Sed est sem, feugiat vitae sem sit amet, cursus facilisis erat. ",
                Drives = new List<Drive>
                {
                    new Ssd
                    {
                        WriteSpeedMBs=500,
                        Interface = "sata",
                        ReadSpeedMBs = 500,
                        Manufacturer = "WD",
                        Tbw = 500
                    },
                    new Ssd
                    {
                        WriteSpeedMBs=4000,
                        Interface = "M.2 NVME 3.0",
                        ReadSpeedMBs = 8000,
                        Manufacturer = "WD",
                        Tbw = 500
                    }
                },
                Gpu = new Gpu
                {
                    ChipManufacturer = GpuManufacturers.Nvidia.ToString(),
                    FrequencyMHz = 1500,
                    Manufacturer = "Asus",
                    MemoryFrequencyMHz = 4000,
                    Name = "GTX 1070",
                    Tdp = 150,
                    VramSizeGB = 8,
                    VramType = GpuVramTypes.GDDR5.ToString(),
                },
                Images = new List<Image>
                {
                    new Image { Location = "https://dlcdnwebimgs.asus.com/gain/75AF54F5-66D8-4A48-B785-65E7F8011C1E/w1000/h732"},
                    new Image { Location = "https://dlcdnwebimgs.asus.com/gain/EA122891-9FE2-4ECF-AAAE-5DAC22E34B1E/w717/h525"},
                    new Image { Location = "https://dlcdnwebimgs.asus.com/gain/A385A525-C371-42CC-8A4C-07900772BFF6/w1000/h732"},
                },
                IsPublic = true,
                IsRemoved = false,
                Name = "ROG Strix GT15 G15 SUPER FAST LEGEND PRO MAX UBER",
                Price = 5000.25m,
                Psu = new Psu {Manufacturer = "Seasonic", Power = 500},
                PriceBeforeDiscount = 6000,
                Ram = new Ram
                {
                    Manufacturer = "Corsair",
                    Name = "LPX 100",
                    FrequencyMHz = 3000,
                    LatencyCL = 17,
                    ModulesNumber = 2,
                    RamTechnology = RamTechnologies.DDR4.ToString()
                },
                Motherboard = new Motherboard
                {
                    Manufacturer = "Asus",
                    Chipset = "B550",
                    Name = "Pro Art",
                    RamType = RamTechnologies.DDR4.ToString(),
                    Socket = "AM4",
                    Usb3Gen1Count = 1,
                    Usb3Gen2Count = 2,
                    UsbCCount = 3
                },
                Cooler = new Cooler
                {
                    Manufacturer = "SPC",
                    CompatibleSockets = new List<string>{"AM4" },
                    FansCount = 2,
                    MaxTdp = 130,
                    Name = "GRANDIS 3"
                },
                DesktopCase = new DesktopCase
                {
                    Manufacturer = "Phanteks",
                    Name = "P400",
                    Widthmm = 200,
                    Heightmm = 400,
                    Lenghtmm = 500,
                    MaxCoolerHeightmm = 165,
                    MaxFanCount = 4,
                    MaxRadiatorSizemm = 280,
                    SupportedMoboSizes = new List<string>{"ATX", MotherboardSizes.MiniATX},
                }
            },
            new DesktopPsuProduct
            {
                Manufacturer = PsuManufacturers.SilverStone.ToString(),
                Name = "SUPER TURBO",
                WarantyMonths = 1,
                FansCount = 0,
                Description = "wspaniały zasilacz rodem z bawarii szlakiem kukurydzy",
                Certificate = PsuCertificates.Titanium,
                MolexCount = 4,
                Images = new List<Image>
                {
                    new Image { Location = "https://cdn.x-kom.pl/i/setup/images/prod/big/product-new-big,,2017/12/pr_2017_12_22_11_13_23_114_03.jpg"},
                },
                Power = 600,
                Price = 400,
                SataCount = 69,
                Pcie6pinCount = 6,
                Protections = PsuProtections.List,
                ExtraInfo = new()
                {
                    new Prop{Name = "BLABLEBUCHA", Value = "40"},
                    new Prop{Name = "AKWKWAD", Value = "nice"},
                    new Prop{Name = "BLABdawdawdawdLEBUCHA", Value = "2"},
                }
            },
            new DesktopGpuProduct
            {
                WarantyMonths=12,
                ChipManufacturer = GpuManufacturers.Amd.ToString(),
                Description = "AMAMAMMAMMAAMMMAAZING GPU FOR GEJMERS",
                FrequencyMHz = 5000,
                Images = new(){ new Image { Location = "https://asset.msi.com/resize/image/global/product/product_0_20180417114910_5ad56eb6add6d.png62405b38c58fe0f07fcef2367d8a9ba1/1024.png"},
                                new Image { Location = "https://asset.msi.com/resize/image/global/product/product_5_20160630092113_5774740910945.png62405b38c58fe0f07fcef2367d8a9ba1/1024.png"},},
                IsPublic = true,
                Lenghtmm = 400,
                Manufacturer = "MSI",
                MemoryFrequencyMHz = 5000,
                Price = 8000,
                PriceBeforeDiscount = 7000,
                Name = "GTX 1070 fe",
                Tdp = 200,
                VramSizeGB = 8,
                VramType = GpuVramTypes.XGDDR5.ToString()
            },
            new LaptopProduct
            {
                WarantyMonths = 12,
                BatteryCapacitymAh = 4000,
                Cpu = new Cpu
                {
                    CoresCount=8,
                    ThreadsCount = 16,
                    FrequencyMHz = 2500,
                    Manufacturer = "AMD",
                    L3CacheMB = 25,
                    Name = "Ryzen 7 2700",
                    Tdp = 150,
                },
                Drives = new List<Drive>
                {
                    new Ssd
                    {
                        WriteSpeedMBs=500,
                        Interface = "sata",
                        ReadSpeedMBs = 500,
                        Manufacturer = "WD",
                        Tbw = 500
                    },
                    new Ssd
                    {
                        WriteSpeedMBs=4000,
                        Interface = "M.2 NVME 3.0",
                        ReadSpeedMBs = 8000,
                        Manufacturer = "WD",
                        Tbw = 500
                    }
                },
                DisplaySize = 17.3m,
                Description = "FAjny laptop gamingnowywyywywyyd",
                Gpu = new Gpu
                {
                    ChipManufacturer = GpuManufacturers.Nvidia.ToString(),
                    FrequencyMHz = 1500,
                    Manufacturer = "Asus",
                    MemoryFrequencyMHz = 4000,
                    Name = "GTX 1070",
                    Tdp = 150,
                    VramSizeGB = 8,
                    VramType = GpuVramTypes.GDDR5.ToString(),
                },
                Manufacturer = "asus",
                 Images = new List<Image>
                {
                    new Image { Location = "https://dlcdnwebimgs.asus.com/gain/d4080303-afe4-4cac-a91c-305a56b63455/"},
                    new Image { Location = "https://dlcdnwebimgs.asus.com/gain/EA122891-9FE2-4ECF-AAAE-5DAC22E34B1E/w717/h525"},
                },
                 Name = "VIVO BOOOK",
                 Motherboard = new Motherboard()
                 {
                     Chipset = "B550",
                     RamSlotsCount = 2,
                     Socket = "BGA 2011",
                     RamType = RamTechnologies.DDR4.ToString(),
                 },
                 Price = 9999,
                 Psu = new Psu(){ Manufacturer="Asus", Power = 120},
                 Ram = new Ram
                {
                    Manufacturer = "Corsair",
                    Name = "LPX 100",
                    FrequencyMHz = 3000,
                    LatencyCL = 17,
                    ModulesNumber = 2,
                    RamTechnology = RamTechnologies.DDR4.ToString()
                },

            },
            new CpuProduct
            {
                WarantyMonths = 24,
                CoresCount = 18,
                Description = "CPU GAMING AMDAAWDAD",
                FrequencyMHz = 6000,
                Images = new List<Image>{new Image { Location= "https://www.intel.pl/content/dam/www/central-libraries/us/en/images/12thgen-promo.jpg.rendition.intel.web.480.270.jpg" } },
                L3CacheMB = 50,
                Manufacturer = "AMD",
                Name = "2700x",
                Price = 3000,
                PriceBeforeDiscount = 5000,
                SupportedChipsets = new(){"B450, B550, X370, X470"},
                SupportedSocket = "LGA 2011",
                Tdp = 500,
                ThreadsCount = 36,
            },
            new MotherboardProduct
            {
                Chipset = "B660",
                Description = "MOBO",
                Images = new(){new Image { Location="https://asset.msi.com/resize/image/global/product/product_3_20190716132640_5d2d6010d11e8.png62405b38c58fe0f07fcef2367d8a9ba1/1024.png"},
                               new Image { Location="https://asset.msi.com/resize/image/global/product/product_1_20190716132640_5d2d60107d8bc.png62405b38c58fe0f07fcef2367d8a9ba1/1024.png"}},
                M2SlotsCount = 2,
                Manufacturer = "MSI",
                PcieX16SlotsCount = 24,
                Price = 800,
                RamSlotsCount = 4,
                RamType = RamTechnologies.DDR4.ToString(),
                SataConnectorsCount = 4,
                Name = "Gaming Plus Max",
                Size = MotherboardSizes.FlexATX,
                Socket = "LGA 2011"
            },
            new RamProduct
            {
                LatencyCL = 50,
                Description = "RAMMMMAAMAMMAMAMA",
                FrequencyMHz = 3200,
                Images = new(){new Image {Location="https://www.corsair.com/medias/sys_master/images/images/hf5/h6f/9110987210782/-CMK16GX4M2B3200C16-Gallery-VENG-LPX-BLK-00.png"}},
                ModulesNumber = 2,
                RamTechnology = RamTechnologies.DDR4.ToString(),
                Name = "LPX 100",
                Manufacturer = RamManufacturers.HyperX.ToString(),
                Price= 700,
                PriceBeforeDiscount = 760,
            },
            new SsdProduct
            {
                WriteSpeedMBs = 200,
                CapacityGB = 40,
                Description = "FAST SSD",
                Images = new List<Image>{new Image { Location= "images/default_image.png" } },
                Interface = "M.2",
                Manufacturer = "Transcend",
                ReadSpeedMBs = 500,
                Name = "MX500",
                Tbw = 500,
                Price = 600,
            },
            new HddProduct
            {
                WriteSpeedMBs = 200,
                CapacityGB = 40,
                Description = "FAST SSD",
                Images = new List<Image>{new Image { Location= "images/default_image.png" } },
                Manufacturer = "Transcend",
                ReadSpeedMBs = 500,
                Name = "P300",
                Price = 600,
                Rpm = 7200,
            },
            new HddProduct
            {
                WriteSpeedMBs = 200,
                CapacityGB = 40,
                Description = "FAST SSD",
                Images = new List<Image>{new Image { Location= "images/default_image.png" } },
                Manufacturer = "Transcend",
                ReadSpeedMBs = 500,
                Name = "HTTP",
                Price = 600,
                Rpm = 7200,
            },
            new HddProduct
            {
                WriteSpeedMBs = 200,
                CapacityGB = 40,
                Description = "FAST SSD",
                Images = new List<Image>{new Image { Location= "images/default_image.png" } },
                Manufacturer = "Transcend",
                ReadSpeedMBs = 500,
                Name = "SADAM",
                Price = 600,
                Rpm = 7200,
            },
            new HddProduct
            {
                WriteSpeedMBs = 200,
                CapacityGB = 40,
                Description = "FAST SSD",
                Images = new List<Image>{new Image { Location= "images/default_image.png" } },
                Manufacturer = "Transcend",
                ReadSpeedMBs = 500,
                Name = "HTTPS",
                Price = 600,
                Rpm = 7200,
            },
            new HddProduct
            {
                WriteSpeedMBs = 200,
                CapacityGB = 40,
                Description = "FAST SSD",
                Images = new List<Image>{new Image { Location= "images/default_image.png" } },
                Manufacturer = "Transcend",
                ReadSpeedMBs = 500,
                Name = "SSL",
                Price = 600,
                Rpm = 7200,
            },
            new HddProduct
            {
                WriteSpeedMBs = 200,
                CapacityGB = 40,
                Description = "FAST SSD",
                Images = new List<Image>{new Image { Location= "images/default_image.png" } },
                Manufacturer = "Transcend",
                ReadSpeedMBs = 500,
                Name = "FTP",
                Price = 600,
                Rpm = 7200,
            },
            new HddProduct
            {
                WriteSpeedMBs = 200,
                CapacityGB = 40,
                Description = "FAST SSD",
                Images = new List<Image>{new Image { Location= "images/default_image.png" } },
                Manufacturer = "Transcend",
                ReadSpeedMBs = 500,
                Name = "Kjeny",
                Price = 600,
                Rpm = 7200,
            },
            new HddProduct
            {
                WriteSpeedMBs = 200,
                CapacityGB = 40,
                Description = "FAST SSD",
                Images = new List<Image>{new Image { Location= "images/default_image.png" } },
                Manufacturer = "Transcend",
                ReadSpeedMBs = 500,
                Name = "Kajl",
                Price = 600,
                Rpm = 7200,
            },
            new HddProduct
            {
                WriteSpeedMBs = 200,
                CapacityGB = 40,
                Description = "FAST SSD",
                Images = new List<Image>{new Image { Location= "images/default_image.png" } },
                Manufacturer = "Transcend",
                ReadSpeedMBs = 500,
                Name = "Kartman",
                Price = 600,
                Rpm = 7200,
            },
            new HddProduct
            {
                WriteSpeedMBs = 200,
                CapacityGB = 40,
                Description = "FAST SSD",
                Images = new List<Image>{new Image { Location= "images/default_image.png" } },
                Manufacturer = "Stan",
                ReadSpeedMBs = 500,
                Name = "Kajls Mom",
                Price = 600,
                Rpm = 7200,
            },
            new HddProduct
            {
                WriteSpeedMBs = 200,
                CapacityGB = 40,
                Description = "FAST SSD",
                Images = new List<Image>{new Image { Location= "images/default_image.png" } },
                Manufacturer = "Szef",
                ReadSpeedMBs = 500,
                Name = "FTP",
                Price = 600,
                Rpm = 7200,
            },
            new HddProduct
            {
                WriteSpeedMBs = 200,
                CapacityGB = 40,
                Description = "FAST SSD",
                Images = new List<Image>{new Image { Location= "images/default_image.png" } },
                Manufacturer = "Gayfish",
                ReadSpeedMBs = 500,
                Name = "FTP",
                Price = 600,
                Rpm = 7200,
            },
            new HddProduct
            {
                WriteSpeedMBs = 200,
                CapacityGB = 40,
                Description = "FAST SSD",
                Images = new List<Image>{new Image { Location= "images/default_image.png" } },
                Manufacturer = "ROMAND",
                ReadSpeedMBs = 500,
                Name = "FTP",
                Price = 600,
                Rpm = 7200,
            },
            new DesktopCaseProduct
            {
                WarantyMonths = 12,
                Widthmm = 400,
                Description = "ahjeihaefjhi uhijafijhsfjiashfi huasijhfhsasiu hojafssfaouafhoaf ohuiasosaf",
                Heightmm = 564,
                Images = new List<Image>{new Image { Location= "https://www.phanteks.com/images/product/Eclipse-P400-TG/Special%20Edition%20Red/P400-1.jpg" } },
                Lenghtmm = 645,
                Manufacturer = DesktopCaseManufacturers.Phanteks.ToString(),
                MaxCoolerHeightmm = 160,
                MaxFanCount = 4,
                MaxGpuLenghtmm = 400,
                MaxRadiatorSizemm = 280,
                Name = "P400A",
                Price = 500,
                PriceBeforeDiscount = 500,
                SupportedMoboSizes = new List<string>{ MotherboardSizes.MicroATX, MotherboardSizes.ExtendedAtx, MotherboardSizes.StandardAtx},
                UsbPorts = 4,
            },
            new DesktopCoolerProduct
            {
                WarantyMonths= 12,
                CompatibleSockets = new(){"AM4", "AM3", "AM3+"},
                FansCount = 1,
                CoolerType = CoolerTypes.BoxCooler,
                Manufacturer = CoolerManufacturers.CoolerMaster.ToString(),
                MaxTdp = 120,
                Name = "Turbo Cooler",
                Price = 200,
                PriceBeforeDiscount = 400,
                Sizemm = 200,
            },
            new CableProduct
            {
                CabelType = "HDMI",
                Color = "Czarny",
                ConnectorA = "HDMI",
                ConnectorB = "HDMI",
                Price = 200,
                PriceBeforeDiscount = 500,
                Manufacturer = "ZOTAC",
                Lenghtmm = 400,
                Name = "HDMI KABEL",
            },
            new ComputerMouseProduct
            {
                Weightg = 120,
                Widthmm = 20,
                IsWireless = true,
                Color = "Czarny",
                Heightmm = 25,
                Lenghtmm = 50,
                Manufacturer = "Logitech",
                Sensor = "Pixart 3061",
                SensorType = "Laser",
                Interface = "USB",
                Name = "G503",
                PollingRateHz = 5000,
                Price = 400
            },
            new KeyboardProduct
            {
                Weightg = 500,
                IsWireless = true,
                Widthmm = 500,
                Color = "black",
                KeyboardType = "Membranowa",
                Manufacturer = "HyperX",
                Price = 540,
                Size = "TKL",
                Name = "Alloy Origin"
            },
            new HeadphonesProduct
            {
                Weightg = 400,
                Widthmm = 500,
                ImpedanceOhm = 40,
                Manufacturer = "Beyerdynamic",
                Name = "BT990",
                PriceBeforeDiscount = 500,
                Price = 400,
                MaxFrequencyResponsekHz = 40,
                MinFrequencyResponseHz = 5,
                Interface = "Jack",
                HeadphonesType = "Otwarte",
            },
            new MonitorProduct
            {
                BrightnessCdm = 40,
                Manufacturer = "ASUS",
                Name = "PRO ART @&!27272",
                PanelSizeInch = 45,
                Ports = new List<string>{ GpuPorts.VGA.ToString(), GpuPorts.DispalyPort.ToString()},
                Contrast = 4000,
                PanelType = "IPS",
                RefreshRateHz = 60,
                ResponseTimems =  4,
                Price = 60,
                ResolutionXpx = 1600,
                ResolutionYpx = 800,
                SrgbColorSpacePerc = 99,
            },
            new PrinterProduct
            {
                HasWiFi = true,
                Color = "Black",
                HasMulticolour = true,
                HasScanner = false,
                Interfaces = new List<string>{"USB", "Blutucz"},
                Manufacturer = "Brother",
                Name = "Ładnie drukuje",
                Technology = "Laserowo",
                Price = 6000,
                SupportedFormats = new List<string>{"A4", "A3", "A2"},
                PrintSpeedPgpmin = 500,
            },
            new ToolProduct
            {
                Manufacturer = "InLine",
                Price = 5600,
                ToolType = "Powietrze sprynżone",
                PriceBeforeDiscount = 200,
                Name = "Silne powietrze jak Kielce",
            }
        };
        private IProductData productsData;
        private readonly ICategoryData categoryData;

        public ProductsService(IProductData productsData, ICategoryData categoryData)
        {
            this.productsData = productsData;
            this.categoryData = categoryData;
        }

        public async Task<Product?> GetProductByIdAsync(string id)
        {
            if (Products == null || Products.Count == 0)
            {
                await GetAllProductsAsync();
                if (Products == null || Products.Count == 0)
                {
                    return null;
                }
            }
            return Products.FirstOrDefault(x => x.Id != null && x.Id.Equals(id));
        }
        public async Task<List<Product>> GetAllProductsAsync()
        {
            foreach (var item in Products)
            {
                item.Category = (await categoryData.GetAllCategoriesAsync()).FirstOrDefault(x => x.Url.Equals(""));
                await productsData.AddProductAsync(item);
            }
            return Products;
        }
        public async Task<List<Product>> GetHighlightedProductsAsync()
        {
            return Products.Where(p => p.IsHiglighted).ToList();
        }
        public async Task<ProductsResponse> GetProductsByCategoryIdAsync(string id, int pageNumber = 1)
        {
            List<Product>? products = Products.Where(x => x.Category.Id.Equals(id)).ToList();
            return GetProductsResponse(products, pageNumber);
        }
    
        public async Task<ProductsResponse> GetProductsByCategoryUrlAsync(string url, int pageNumber = 1)
        {
            List<Product>? products = Products.Where(x => x.Category?.Name?.Equals(url) ?? false).ToList();
            return GetProductsResponse(products, pageNumber);
        }
        public async Task<ProductsResponse> FindProductsByTextAsync(string text, int pageNumber = 1)
        {
            List<Product>? foundProducts = FindProducts(text);
            return GetProductsResponse(foundProducts, pageNumber);
        }

        public async Task<List<string>> GetProductsSuggestionsByTextAsync(string text)
        {
            text = text.ToLower();
            List<string> suggestions = new();
            List<Product> products = FindProducts(text);
            if(products == null || products.Count == 0)
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

        private List<Product> FindProducts(string text)
        {
            text = text.ToLower();
            List<string>? words = text.Split(' ').ToList();
            List<Product> products = new();

            words.ForEach(word => products.AddRange(Products
                .Where(x => (x.Manufacturer != null && x.Manufacturer.Contains(word, StringComparison.OrdinalIgnoreCase)) ||
                            (x.Name != null && x.Name.Contains(word, StringComparison.OrdinalIgnoreCase)) ||
                            (x.Description != null && x.Description.Contains(word, StringComparison.OrdinalIgnoreCase)))));
            return products;
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
            else if(products == null)
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

        public async Task<List<Product>> GetProductsByIdListAsync(List<string> idList)
        {
            List<Product> products = new();
            await Task.Run(() => idList.ForEach(async id =>
            {
                var prod = await GetProductByIdAsync(id);
                if (prod != null)
                    products.Add(prod);
            }));
            return products;
        }
    }
}
