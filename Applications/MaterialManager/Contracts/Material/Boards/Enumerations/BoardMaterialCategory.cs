using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Converter;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace HomagConnect.MaterialManager.Contracts.Material.Boards.Enumerations
{
    /// <summary>
    /// Board material category
    /// </summary>
    [ResourceManager(typeof(BoardMaterialCategoryDisplayNames))]
    [JsonConverter(typeof(TolerantEnumConverter))]
    public enum BoardMaterialCategory
    {
        // ReSharper disable InconsistentNaming
        // ReSharper disable IdentifierTypo
        
        /// <summary>
        /// Acrylic composite materials.
        /// </summary>
        [TypicalDensity(1800)]
        [Display(Description = "Acrylic composite materials")]
        AcrylicCompositeMaterials,

        /// <summary>
        /// Acrylic glass (PMMA).
        /// </summary>
        [TypicalDensity(1180)]
        [Display(Description = "Acrylic glass (PMMA)")]
        AcrylicGlass_PMMA,

        /// <summary>
        /// Al-MG-Cu.
        /// </summary>
        [TypicalDensity(2700)]
        [Display(Description = "Al-MG-Cu")]
        AlMgCu,

        /// <summary>
        /// Al-Si alloy.
        /// </summary>
        [TypicalDensity(2650)]
        [Display(Description = "Al-Si alloy")]
        AlSiAlloy,

        /// <summary>
        /// Aluminum.
        /// </summary>
        [TypicalDensity(2700)]
        [Display(Description = "Aluminum")]
        Aluminum,

        /// <summary>
        /// Aluminum honeycomb panels.
        /// </summary>
        [TypicalDensity(200)]
        [Display(Description = "Aluminum honeycomb panels")]
        AluminumHoneycombPanels,

        /// <summary>
        /// Blockboard and laminboard (hard wood).
        /// </summary>
        [TypicalDensity(700)]
        [Display(Description = "Blockboard and laminboard (hard wood)")]
        BlockboardAndLaminboard_HardWood,

        /// <summary>
        /// Blockboard and laminboard (soft wood).
        /// </summary>
        [TypicalDensity(450)]
        [Display(Description = "Blockboard and laminboard (soft wood)")]
        BlockboardAndLaminboard_SoftWood,

        /// <summary>
        /// Butyl rubber (IIR).
        /// </summary>
        [TypicalDensity(935)]
        [Display(Description = "Butyl rubber (IIR)")]
        ButylRubber_IIR,

        /// <summary>
        /// Carbon-fibre-reinforced plastics (CFK).
        /// </summary>
        [TypicalDensity(1575)]
        [Display(Description = "Carbon-fibre-reinforced plastics (CFK)")]
        CarbonfibrereinforcedPlastics_CFK,

        /// <summary>
        /// Cement-bound panels
        /// </summary>
        [TypicalDensity(2000)]
        [Display(Description = "Cement-bound panels")]
        CementboundPanels,

        /// <summary>
        /// Chipboard.
        /// </summary>
        [TypicalDensity(650)]
        [Display(Description = "Chipboard")]
        Chipboard,

        /// <summary>
        /// Co-polymer-bound mineral materials.
        /// </summary>
        [TypicalDensity(1800)]
        [Display(Description = "Co-polymer-bound mineral materials")]
        CopolymerboundMineralMaterials,

        /// <summary>
        /// Compact panels (HPL).
        /// </summary>
        [TypicalDensity(1300)]
        [Display(Description = "Compact panels (HPL)")]
        CompactPanels_HPL,

        /// <summary>
        /// Composite panels with aluminum.
        /// </summary>
        [TypicalDensity(2250)]
        [Display(Description = "Composite panels with aluminum")]
        CompositePanelsWithAluminum,

        /// <summary>
        /// Copper, zinc, brass.
        /// </summary>
        [TypicalDensity(8700)]
        [Display(Description = "Copper, zinc, brass")]
        CopperZincBrass,

        /// <summary>
        /// Fibreglass-reinforced plastics (GFRP).
        /// </summary>
        [TypicalDensity(1900)]
        [Display(Description = "Fibreglass-reinforced plastics (GFRP)")]
        FibreglassReinforcedPlastics_GFRP,

        /// <summary>
        /// Flakeboard (OSB panel).
        /// </summary>
        [TypicalDensity(650)]
        [Display(Description = "Flakeboard (OSB panel)")]
        Flakeboard_OSBPanel,

        /// <summary>
        /// Glued wood panel/glue-laminated wood (hard wood).
        /// </summary>
        [TypicalDensity(750)]
        [Display(Description = "Glued wood panel/glue-laminated wood (hard wood)")]
        GluedWoodPanelGluelaminatedWood_HardWood,

        /// <summary>
        /// Glued wood panel/glue-laminated wood (soft wood).
        /// </summary>
        [TypicalDensity(450)]
        [Display(Description = "Glued wood panel/glue-laminated wood (soft wood)")]
        GluedWoodPanelGluelaminatedWood_SoftWood,

        /// <summary>
        /// Gypsum plasterboard (plasterboard).
        /// </summary>
        [TypicalDensity(800)]
        [Display(Description = "Gypsum plasterboard (plasterboard)")]
        GypsumPlasterboard_Plasterboard,

        /// <summary>
        /// Hard foam panel.
        /// </summary>
        [TypicalDensity(65)]
        [Display(Description = "Hard foam panel")]
        HardFoamPanel,

        /// <summary>
        /// Hard paper (HP).
        /// </summary>
        [TypicalDensity(1000)]
        [Display(Description = "Hard paper (HP)")]
        HardPaper_HP,

        /// <summary>
        /// Hard particle board.
        /// </summary>
        [TypicalDensity(950)]
        [Display(Description = "Hard particle board")]
        HardParticleBoard,

        /// <summary>
        /// Hardwood crosswise.
        /// </summary>
        [TypicalDensity(750)]
        [Display(Description = "Hardwood crosswise")]
        HardwoodCrosswise,

        /// <summary>
        /// Hardwood lengthwise.
        /// </summary>
        [TypicalDensity(750)]
        [Display(Description = "Hardwood lengthwise")]
        HardwoodLengthwise,

        /// <summary>
        /// High-density fiberboard (HDF).
        /// </summary>
        [TypicalDensity(900)]
        [Display(Description = "High-density fiberboard (HDF)")]
        HighdensityFiberboard_HDF,

        /// <summary>
        /// Integral foam panel.
        /// </summary>
        [TypicalDensity(200)]
        [Display(Description = "Integral foam panel")]
        IntegralFoamPanel,

        /// <summary>
        /// Laminate.
        /// </summary>
        [TypicalDensity(900)]
        [Display(Description = "Laminate")]
        Laminate,

        /// <summary>
        /// Laminate (HPL).
        /// </summary>
        [TypicalDensity(1300)]
        [Display(Description = "Laminate (HPL)")]
        Laminate_HPL,

        /// <summary>
        /// Laminate flooring panels.
        /// </summary>
        [TypicalDensity(900)]
        [Display(Description = "Laminate flooring panels")]
        LaminateFlooringPanels,

        /// <summary>
        /// Laminated fabric (HGW).
        /// </summary>
        [TypicalDensity(1500)]
        [Display(Description = "Laminated fabric (HGW)")]
        LaminatedFabric_HGW,

        /// <summary>
        /// Lead alloy.
        /// </summary>
        [TypicalDensity(11000)]
        [Display(Description = "Lead alloy")]
        LeadAlloy,

        /// <summary>
        /// Lightweight panel with chipboard.
        /// </summary>
        [TypicalDensity(400)]
        [Display(Description = "Lightweight panel with chipboard")]
        LightweightPanelWithChipboard,

        /// <summary>
        /// Lightweight panel with MDF boards.
        /// </summary>
        [TypicalDensity(500)]
        [Display(Description = "Lightweight panel with MDF boards")]
        LightweightPanelWithMdfBoards,

        /// <summary>
        /// Low-melting thermoplastics (PP, PA).
        /// </summary>
        [TypicalDensity(1025)]
        [Display(Description = "Low-melting thermoplastics (PP, PA)")]
        LowmeltingThermoplastics_PP_PA,

        /// <summary>
        /// Medium-density fiberboard (MDF).
        /// </summary>
        [TypicalDensity(700)]
        [Display(Description = "Medium-density fiberboard (MDF)")]
        MediumdensityFiberboard_MDF,

        /// <summary>
        /// Natural rubber (NR).
        /// </summary>
        [TypicalDensity(930)]
        [Display(Description = "Natural rubber (NR)")]
        NaturalRubber_NR,

        /// <summary>
        /// Phenol formaldehyde (PF).
        /// </summary>
        [TypicalDensity(1350)]
        [Display(Description = "Phenol formaldehyde (PF)")]
        PhenolFormaldehyde_PF,

        /// <summary>
        /// Phenolic resin coated plywood.
        /// </summary>
        [TypicalDensity(650)]
        [Display(Description = "Phenolic resin coated plywood")]
        PhenolicResinCoatedPlywood,

        /// <summary>
        /// Plastic honeycomb panels.
        /// </summary>
        [TypicalDensity(175)]
        [Display(Description = "Plastic honeycomb panels")]
        PlasticHoneycombPanels,

        /// <summary>
        /// Plywood (multiplex panel).
        /// </summary>
        [TypicalDensity(650)]
        [Display(Description = "Plywood (multiplex panel)")]
        Plywood_MultiplexPanel,

        /// <summary>
        /// Polyester-bound mineral materials.
        /// </summary>
        [TypicalDensity(1800)]
        [Display(Description = "Polyester-bound mineral materials")]
        PolyesterboundMineralMaterials,

        /// <summary>
        /// Polyvinyl carbazole (PVK).
        /// </summary>
        [TypicalDensity(1200)]
        [Display(Description = "Polyvinyl carbazole (PVK)")]
        PolyvinylCarbazole_PVK,

        /// <summary>
        /// Soft fiberboard.
        /// </summary>
        [TypicalDensity(300)]
        [Display(Description = "Soft fiberboard")]
        SoftFiberboard,

        /// <summary>
        /// Softwood crosswise.
        /// </summary>
        [TypicalDensity(450)]
        [Display(Description = "Softwood crosswise")]
        SoftwoodCrosswise,

        /// <summary>
        /// Softwood lengthwise.
        /// </summary>
        [TypicalDensity(450)]
        [Display(Description = "Softwood lengthwise")]
        SoftwoodLengthwise,

        /// <summary>
        /// Styrene-butadiene rubber (SBR).
        /// </summary>
        [TypicalDensity(925)]
        [Display(Description = "Styrene-butadiene rubber (SBR)")]
        StyrenebutadieneRubber_SBR,

        /// <summary>
        /// Thermoplastics (PE, PVC, ABS, POM, PS).
        /// </summary>
        [TypicalDensity(1150)]
        [Display(Description = "Thermoplastics (PE, PVC, ABS, POM, PS)")]
        Thermoplastics,

        /// <summary>
        /// Three-layer panel, laminated timber panels (hard wood).
        /// </summary>
        [TypicalDensity(700)]
        [Display(Description = "Three-layer panel, laminated timber panels (hard wood)")]
        ThreelayerPanelLaminatedTimberPanels_HardWood,

        /// <summary>
        /// Three-layer panel, laminated timber panels (soft wood).
        /// </summary>
        [TypicalDensity(450)]
        [Display(Description = "Three-layer panel, laminated timber panels (soft wood)")]
        ThreelayerPanelLaminatedTimberPanels_SoftWood,

        /// <summary>
        /// Undefined.
        /// </summary>
        [TypicalDensity(500)]
        [Display(Description = "Undefined")]
        Undefined,

        /// <summary>
        /// Veneer.
        /// </summary>
        [TypicalDensity(700)]
        [Display(Description = "Veneer")]
        Veneer,

        /// <summary>
        /// Wood-plastic composites (WPC).
        /// </summary>
        [TypicalDensity(1200)]
        [Display(Description = "Wood-plastic composites (WPC)")]
        WoodplasticComposites_WPC,

        // ReSharper restore IdentifierTypo
        // ReSharper restore InconsistentNaming
    }

    
}
