using ComputerShop.Shared.Models;
using ComputerShop.Shared.Models.Components;
using ComputerShop.Shared.Models.Products;

namespace ComputerShop.Server.Services
{
    public class ProductsService : IProductsService
    {
        public List<Product> Products { get; set; } = new List<Product>
        {
            new DesktopPcProduct
            { 
                Id = 1,
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
                Name = "ROG Strix GT15 G15",
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
                    SupportedMoboSizes = new List<string>{"ATX", MotherboardSizes.MiniATX}
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
                Id = 66,
                Pcie6pinCount = 6,
                Protections = PsuProtections.List,
                ExtraInfo = new()
                {
                    new Prop{Name = "BLABLEBUCHA", Value = "40"},
                    new Prop{Name = "AKWKWAD", Value = "nice"},
                    new Prop{Name = "BLABdawdawdawdLEBUCHA", Value = "2"},
                }
            },
             new DesktopPcProduct
            {
                Id = 1,
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
                Name = "ROG Strix GT15 G15",
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
                    SupportedMoboSizes = new List<string>{"ATX", MotherboardSizes.MiniATX}
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
                Id = 66,
                Pcie6pinCount = 6,
                Protections = PsuProtections.List,
                ExtraInfo = new()
                {
                    new Prop{Name = "BLABLEBUCHA", Value = "40"},
                    new Prop{Name = "AKWKWAD", Value = "nice"},
                    new Prop{Name = "BLABdawdawdawdLEBUCHA", Value = "2"},
                }
            },
             new DesktopPcProduct
            {
                Id = 1,
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
                Name = "ROG Strix GT15 G15",
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
                    SupportedMoboSizes = new List<string>{"ATX", MotherboardSizes.MiniATX}
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
                Id = 66,
                Pcie6pinCount = 6,
                Protections = PsuProtections.List,
                ExtraInfo = new()
                {
                    new Prop{Name = "BLABLEBUCHA", Value = "40"},
                    new Prop{Name = "AKWKWAD", Value = "nice"},
                    new Prop{Name = "BLABdawdawdawdLEBUCHA", Value = "2"},
                }
            },
            new DesktopPcProduct
            {
                Id = 1,
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
                Name = "ROG Strix GT15 G15",
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
                    SupportedMoboSizes = new List<string>{"ATX", MotherboardSizes.MiniATX}
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
                Id = 66,
                Pcie6pinCount = 6,
                Protections = PsuProtections.List,
                ExtraInfo = new()
                {
                    new Prop{Name = "BLABLEBUCHA", Value = "40"},
                    new Prop{Name = "AKWKWAD", Value = "nice"},
                    new Prop{Name = "BLABdawdawdawdLEBUCHA", Value = "2"},
                }
            },
             new DesktopPcProduct
            {
                Id = 1,
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
                Name = "ROG Strix GT15 G15",
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
                    SupportedMoboSizes = new List<string>{"ATX", MotherboardSizes.MiniATX}
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
                Id = 66,
                Pcie6pinCount = 6,
                Protections = PsuProtections.List,
                ExtraInfo = new()
                {
                    new Prop{Name = "BLABLEBUCHA", Value = "40"},
                    new Prop{Name = "AKWKWAD", Value = "nice"},
                    new Prop{Name = "BLABdawdawdawdLEBUCHA", Value = "2"},
                }
            },
             new DesktopPcProduct
            {
                Id = 1,
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
                Name = "ROG Strix GT15 G15",
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
                    SupportedMoboSizes = new List<string>{"ATX", MotherboardSizes.MiniATX}
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
                Id = 66,
                Pcie6pinCount = 6,
                Protections = PsuProtections.List,
                ExtraInfo = new()
                {
                    new Prop{Name = "BLABLEBUCHA", Value = "40"},
                    new Prop{Name = "AKWKWAD", Value = "nice"},
                    new Prop{Name = "BLABdawdawdawdLEBUCHA", Value = "2"},
                }
            },
        };
                

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            if(Products == null || Products.Count == 0)
            {
                await GetAllProductsAsync();
                if (Products == null || Products.Count == 0)
                {
                    return null;
                }                
            }
            return Products.FirstOrDefault(x => x.Id == id);
        }
        public async Task<List<Product>> GetAllProductsAsync()
        {
            return Products;
        }
        public async Task<List<Product>> GetProductsByCategoryIdAsync(int id)
        {
            return Products.Where(x => x.Category.Id == id.ToString()).ToList(); //tmp to string
        }
        public async Task<List<Product>> GetProductsByCategoryUrlAsync(string url)
        {
            return Products.Where(x => x.Category?.Name == url).ToList();
        }
    }
}
