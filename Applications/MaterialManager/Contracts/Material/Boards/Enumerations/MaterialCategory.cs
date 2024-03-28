using System.ComponentModel.DataAnnotations;

namespace HomagConnect.MaterialManager.Contracts.Material.Boards.Enumerations
{
    public enum MaterialCategory
    {
        [Display(Description = "Acrylic composite materials")]
        MW_AB,

        [Display(Description = "Acrylic glass (PMMA)")]
        TP_PMMA,

        [Display(Description = "Al-MG-Cu")]
        NEM_AMC,

        [Display(Description = "Al-Si alloy")]
        NEM_ASL,

        [Display(Description = "Aluminum")]
        NEM_A,

        [Display(Description = "Aluminum honeycomb panels")]
        NEM_AW,

        [Display(Description = "Blockboard and laminboard (hard wood)")]
        SF_SS_H,

        [Display(Description = "Blockboard and laminboard (soft wood)")]
        SF_SS_W,

        [Display(Description = "Butyl rubber (IIR)")]
        E_IIR,

        [Display(Description = "Carbon-fibre-reinforced plastics (CFK)")]
        FK_CFK,

        [Display(Description = "Cement-bound panels")]
        V_ZG,

        [Display(Description = "Chipboard")]
        HH_SP,

        [Display(Description = "Co-polymer-bound mineral materials")]
        MW_CPB,

        [Display(Description = "Compact panels (HPL)")]
        DP_HPL,

        [Display(Description = "Composite panels with aluminum")]
        V_A,

        [Display(Description = "Copper, zinc, brass")]
        NEM_KZM,

        [Display(Description = "Fibreglass-reinforced plastics (GFRP)")]
        FK_GFK,

        [Display(Description = "Flakeboard (OSB panel)")]
        HH_GSP,

        [Display(Description = "Glued wood panel/glue-laminated wood (hard wood)")]
        SF_LHP_BSP_H,

        [Display(Description = "Glued wood panel/glue-laminated wood (soft wood)")]
        SF_LHP_BSP_W,

        [Display(Description = "Gypsum plasterboard (plasterboard)")]
        V_GK,

        [Display(Description = "Hard foam panel")]
        SS_EPS,

        [Display(Description = "Hard paper (HP)")]
        DP_HP,

        [Display(Description = "Hard particle board")]
        HH_HFP,

        [Display(Description = "Hardwood crosswise")]
        VHWS_HQ,

        [Display(Description = "Hardwood lengthwise")]
        VHWS_HL,

        [Display(Description = "High-density fiberboard (HDF)")]
        HH_HDF,

        [Display(Description = "Integral foam panel")]
        SS_PUR,

        [Display(Description = "Laminate")]
        S_L,

        [Display(Description = "Laminate (HPL)")]
        DP_HPL_SCH,

        [Display(Description = "Laminate flooring panels")]
        V_LFP,

        [Display(Description = "Laminated fabric (HGW)")]
        DP_HGW,

        [Display(Description = "Lead alloy")]
        NEM_BL,

        [Display(Description = "Lightweight panel with chipboard")]
        LB_SP,

        [Display(Description = "Lightweight panel with MDF boards")]
        LB_MDF,

        [Display(Description = "Low-melting thermoplastics (PP, PA)")]
        TP_PA,

        [Display(Description = "Medium-density fiberboard (MDF)")]
        HH_MDF,

        [Display(Description = "Natural rubber (NR)")]
        E_NR,

        [Display(Description = "Phenol formaldehyde (PF)")]
        DP_PF,

        [Display(Description = "Phenolic resin coated plywood")]
        SF_SDP,

        [Display(Description = "Plastic honeycomb panels")]
        KW_KW,

        [Display(Description = "Plywood (multiplex panel)")]
        SF_FSH,

        [Display(Description = "Polyester-bound mineral materials")]
        MW_PB,

        [Display(Description = "Polyvinyl carbazole (PVK)")]
        E_PUK,

        [Display(Description = "Soft fiberboard")]
        HH_WFP,

        [Display(Description = "Softwood crosswise")]
        VHWS_WQ,

        [Display(Description = "Softwood lengthwise")]
        VHWS_WL,

        [Display(Description = "Styrene-butadiene rubber (SBR)")]
        E_SBR,

        [Display(Description = "Thermoplastics (PE, PVC, ABS, POM, PS)")]
        TP_PVC,

        [Display(Description = "Three-layer panel, laminated timber panels (hard wood)")]
        SF_DSP_H,

        [Display(Description = "Three-layer panel, laminated timber panels (soft wood)")]
        SF_DSP_W,

        [Display(Description = "Undefined")]
        U,

        [Display(Description = "Veneer")]
        SF_F,

        [Display(Description = "Wood-plastic composites (WPC)")]
        WPC_WPC
    }
}