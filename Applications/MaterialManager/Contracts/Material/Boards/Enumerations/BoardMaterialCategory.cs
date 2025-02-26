using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Attributes;

namespace HomagConnect.MaterialManager.Contracts.Material.Boards.Enumerations
{
    /// <summary>
    /// Board material category
    /// </summary>
    [ResourceManager(typeof(BoardMatCatDisplayNames))]
    public enum BoardMaterialCategory
    {
        // ReSharper disable InconsistentNaming
        // ReSharper disable IdentifierTypo

        /// <summary>
        /// Acrylic composite materials.
        /// </summary>
        [Display(Description = "Acrylic composite materials")]
        AcrylicCompositeMaterials,

        /// <summary>
        /// Acrylic glass (PMMA).
        /// </summary>
        [Display(Description = "Acrylic glass (PMMA)")]
        AcrylicGlass_PMMA,

        /// <summary>
        /// Al-MG-Cu.
        /// </summary>
        [Display(Description = "Al-MG-Cu")]
        AlMgCu,

        /// <summary>
        /// Al-Si alloy.
        /// </summary>
        [Display(Description = "Al-Si alloy")]
        AlSiAlloy,

        /// <summary>
        /// Aluminum.
        /// </summary>
        [Display(Description = "Aluminum")]
        Aluminum,

        /// <summary>
        /// Aluminum honeycomb panels.
        /// </summary>
        [Display(Description = "Aluminum honeycomb panels")]
        AluminumHoneycombPanels,

        /// <summary>
        /// Blockboard and laminboard (hard wood).
        /// </summary>
        [Display(Description = "Blockboard and laminboard (hard wood)")]
        BlockboardAndLaminboard_HardWood,

        /// <summary>
        /// Blockboard and laminboard (soft wood).
        /// </summary>
        [Display(Description = "Blockboard and laminboard (soft wood)")]
        BlockboardAndLaminboard_SoftWood,

        /// <summary>
        /// Butyl rubber (IIR).
        /// </summary>
        [Display(Description = "Butyl rubber (IIR)")]
        ButylRubber_IIR,

        /// <summary>
        /// Carbon-fibre-reinforced plastics (CFK).
        /// </summary>
        [Display(Description = "Carbon-fibre-reinforced plastics (CFK)")]
        CarbonfibrereinforcedPlastics_CFK,

        /// <summary>
        /// Cement-bound panels
        /// </summary>
        [Display(Description = "Cement-bound panels")]
        CementboundPanels,

        /// <summary>
        /// Chipboard.
        /// </summary>
        [Display(Description = "Chipboard")]
        Chipboard,

        /// <summary>
        /// Co-polymer-bound mineral materials.
        /// </summary>
        [Display(Description = "Co-polymer-bound mineral materials")]
        CopolymerboundMineralMaterials,

        /// <summary>
        /// Compact panels (HPL).
        /// </summary>
        [Display(Description = "Compact panels (HPL)")]
        CompactPanels_HPL,

        /// <summary>
        /// Composite panels with aluminum.
        /// </summary>
        [Display(Description = "Composite panels with aluminum")]
        CompositePanelsWithAluminum,

        /// <summary>
        /// Copper, zinc, brass.
        /// </summary>
        [Display(Description = "Copper, zinc, brass")]
        CopperZincBrass,

        /// <summary>
        /// Fibreglass-reinforced plastics (GFRP).
        /// </summary>
        [Display(Description = "Fibreglass-reinforced plastics (GFRP)")]
        FibreglassReinforcedPlastics_GFRP,

        /// <summary>
        /// Flakeboard (OSB panel).
        /// </summary>
        [Display(Description = "Flakeboard (OSB panel)")]
        Flakeboard_OSBPanel,

        /// <summary>
        /// Glued wood panel/glue-laminated wood (hard wood).
        /// </summary>
        [Display(Description = "Glued wood panel/glue-laminated wood (hard wood)")]
        GluedWoodPanelGluelaminatedWood_HardWood,

        /// <summary>
        /// Glued wood panel/glue-laminated wood (soft wood).
        /// </summary>
        [Display(Description = "Glued wood panel/glue-laminated wood (soft wood)")]
        GluedWoodPanelGluelaminatedWood_SoftWood,

        /// <summary>
        /// Gypsum plasterboard (plasterboard).
        /// </summary>
        [Display(Description = "Gypsum plasterboard (plasterboard)")]
        GypsumPlasterboard_Plasterboard,

        /// <summary>
        /// Hard foam panel.
        /// </summary>
        [Display(Description = "Hard foam panel")]
        HardFoamPanel,

        /// <summary>
        /// Hard paper (HP).
        /// </summary>
        [Display(Description = "Hard paper (HP)")]
        HardPaper_HP,

        /// <summary>
        /// Hard particle board.
        /// </summary>
        [Display(Description = "Hard particle board")]
        HardParticleBoard,

        /// <summary>
        /// Hardwood crosswise.
        /// </summary>
        [Display(Description = "Hardwood crosswise")]
        HardwoodCrosswise,

        /// <summary>
        /// Hardwood lengthwise.
        /// </summary>
        [Display(Description = "Hardwood lengthwise")]
        HardwoodLengthwise,

        /// <summary>
        /// High-density fiberboard (HDF).
        /// </summary>
        [Display(Description = "High-density fiberboard (HDF)")]
        HighdensityFiberboard_HDF,

        /// <summary>
        /// Integral foam panel.
        /// </summary>
        [Display(Description = "Integral foam panel")]
        IntegralFoamPanel,

        /// <summary>
        /// Laminate.
        /// </summary>
        [Display(Description = "Laminate")]
        Laminate,

        /// <summary>
        /// Laminate (HPL).
        /// </summary>
        [Display(Description = "Laminate (HPL)")]
        Laminate_HPL,

        /// <summary>
        /// Laminate flooring panels.
        /// </summary>
        [Display(Description = "Laminate flooring panels")]
        LaminateFlooringPanels,

        /// <summary>
        /// Laminated fabric (HGW).
        /// </summary>
        [Display(Description = "Laminated fabric (HGW)")]
        LaminatedFabric_HGW,

        /// <summary>
        /// Lead alloy.
        /// </summary>
        [Display(Description = "Lead alloy")]
        LeadAlloy,

        /// <summary>
        /// Lightweight panel with chipboard.
        /// </summary>
        [Display(Description = "Lightweight panel with chipboard")]
        LightweightPanelWithChipboard,

        /// <summary>
        /// Lightweight panel with MDF boards.
        /// </summary>
        [Display(Description = "Lightweight panel with MDF boards")]
        LightweightPanelWithMdfBoards,

        /// <summary>
        /// Low-melting thermoplastics (PP, PA).
        /// </summary>
        [Display(Description = "Low-melting thermoplastics (PP, PA)")]
        LowmeltingThermoplastics_PP_PA,

        /// <summary>
        /// Medium-density fiberboard (MDF).
        /// </summary>
        [Display(Description = "Medium-density fiberboard (MDF)")]
        MediumdensityFiberboard_MDF,

        /// <summary>
        /// Natural rubber (NR).
        /// </summary>
        [Display(Description = "Natural rubber (NR)")]
        NaturalRubber_NR,

        /// <summary>
        /// Phenol formaldehyde (PF).
        /// </summary>
        [Display(Description = "Phenol formaldehyde (PF)")]
        PhenolFormaldehyde_PF,

        /// <summary>
        /// Phenolic resin coated plywood.
        /// </summary>
        [Display(Description = "Phenolic resin coated plywood")]
        PhenolicResinCoatedPlywood,

        /// <summary>
        /// Plastic honeycomb panels.
        /// </summary>
        [Display(Description = "Plastic honeycomb panels")]
        PlasticHoneycombPanels,

        /// <summary>
        /// Plywood (multiplex panel).
        /// </summary>
        [Display(Description = "Plywood (multiplex panel)")]
        Plywood_MultiplexPanel,

        /// <summary>
        /// Polyester-bound mineral materials.
        /// </summary>
        [Display(Description = "Polyester-bound mineral materials")]
        PolyesterboundMineralMaterials,

        /// <summary>
        /// Polyvinyl carbazole (PVK).
        /// </summary>
        [Display(Description = "Polyvinyl carbazole (PVK)")]
        PolyvinylCarbazole_PVK,

        /// <summary>
        /// Soft fiberboard.
        /// </summary>
        [Display(Description = "Soft fiberboard")]
        SoftFiberboard,

        /// <summary>
        /// Softwood crosswise.
        /// </summary>
        [Display(Description = "Softwood crosswise")]
        SoftwoodCrosswise,

        /// <summary>
        /// Softwood lengthwise.
        /// </summary>
        [Display(Description = "Softwood lengthwise")]
        SoftwoodLengthwise,

        /// <summary>
        /// Styrene-butadiene rubber (SBR).
        /// </summary>
        [Display(Description = "Styrene-butadiene rubber (SBR)")]
        StyrenebutadieneRubber_SBR,

        /// <summary>
        /// Thermoplastics (PE, PVC, ABS, POM, PS).
        /// </summary>
        [Display(Description = "Thermoplastics (PE, PVC, ABS, POM, PS)")]
        Thermoplastics,

        /// <summary>
        /// Three-layer panel, laminated timber panels (hard wood).
        /// </summary>
        [Display(Description = "Three-layer panel, laminated timber panels (hard wood)")]
        ThreelayerPanelLaminatedTimberPanels_HardWood,

        /// <summary>
        /// Three-layer panel, laminated timber panels (soft wood).
        /// </summary>
        [Display(Description = "Three-layer panel, laminated timber panels (soft wood)")]
        ThreelayerPanelLaminatedTimberPanels_SoftWood,

        /// <summary>
        /// Undefined.
        /// </summary>
        [Display(Description = "Undefined")]
        Undefined,

        /// <summary>
        /// Veneer.
        /// </summary>
        [Display(Description = "Veneer")]
        Veneer,

        /// <summary>
        /// Wood-plastic composites (WPC).
        /// </summary>
        [Display(Description = "Wood-plastic composites (WPC)")]
        WoodplasticComposites_WPC

        // ReSharper restore IdentifierTypo
        // ReSharper restore InconsistentNaming
    }
}