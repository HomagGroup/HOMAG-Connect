﻿using HomagConnect.MaterialManager.Contracts.Material.Edgebands.Enumerations;
using HomagConnect.MaterialManager.Contracts.Request;
using HomagConnect.MaterialManager.Samples.Create.Boards;
using HomagConnect.MaterialManager.Samples.Read.Edgebands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomagConnect.MaterialManager.Tests.Read.Edgebands;

// <summary />
[TestClass]
public class ReadEdgebandTypeTests : MaterialManagerTestBase
{
    /// <summary />
    [ClassInitialize]
    public async Task Initialize()
    {
        var materialManagerClient = GetMaterialManagerClient();
        var edgebandTypeRequest = new MaterialManagerRequestEdgebandType
        {
            EdgebandCode = "ABS_Abruzzo_colore_1.00_100.0_HM",
            Height = 100,
            Thickness = 1.0,
            DefaultLength = 75.0,
            MaterialCategory = EdgebandMaterialCategory.ABS,
            Process = EdgebandingProcess.Other,
        };
        var newEdgebandType = await materialManagerClient.Material.Edgebands.CreateEdgebandType(edgebandTypeRequest);
    }

    [TestMethod]
    public async Task EdgebandsGetEdgebandTypeByEdgebandCode()
    {
        var materialManagerClient = GetMaterialManagerClient();
        var edgebandCode = "ABS_Abruzzo_colore_1.00_100.0_HM";
        await MaterialManagerReadEdgebandResultsSamples.Edgebands_GetEdgebandTypeByEdgebandCode(materialManagerClient.Material.Edgebands, edgebandCode);
    }

    [TestMethod]
    public async Task EdgebandsGetEdgebandTypeByEdgebandCodeIncludingDetails()
    {
        var materialManagerClient = GetMaterialManagerClient();
        var edgebandCode = "ABS_Abruzzo_colore_1.00_100.0_HM";
        await MaterialManagerReadEdgebandResultsSamples.Edgebands_GetEdgebandTypeByEdgebandCodeIncludingDetails(materialManagerClient.Material.Edgebands, edgebandCode);
    }

    [TestMethod]
    public async Task EdgebandsGetEdgebandTypes()
    {
        var materialManagerClient = GetMaterialManagerClient();
        await MaterialManagerReadEdgebandResultsSamples.Edgebands_GetEdgebandTypes(materialManagerClient.Material.Edgebands);
    }

    [TestMethod]
    public async Task EdgebandsGetEdgebandTypesIncludingDetails()
    {
        var materialManagerClient = GetMaterialManagerClient();
        await MaterialManagerReadEdgebandResultsSamples.Edgebands_GetEdgebandTypesIncludingDetails(materialManagerClient.Material.Edgebands);
    }

    [TestMethod]
    public async Task EdgebandsGetEdgebandTypesByEdgebandCodes()
    {
        var materialManagerClient = GetMaterialManagerClient();
        var edgebandCodes = new List<string> { "ABS_Abruzzo_colore_1.00_100.0_HM", "ABS_Black_1.20_23.0_ZJ" };
        await MaterialManagerReadEdgebandResultsSamples.Edgebands_GetEdgebandTypesByEdgebandCodes(materialManagerClient.Material.Edgebands, edgebandCodes);
    }

    [TestMethod]
    public async Task EdgebandsGetEdgebandTypesByEdgebandCodesIncludingDetails()
    {
        var materialManagerClient = GetMaterialManagerClient();
        var edgebandCodes = new List<string> { "ABS_Abruzzo_colore_1.00_100.0_HM", "ABS_Black_1.20_23.0_ZJ" };
        await MaterialManagerReadEdgebandResultsSamples.Edgebands_GetEdgebandTypesByEdgebandCodesIncludingDetails(materialManagerClient.Material.Edgebands, edgebandCodes);
    }
    
    
    [TestMethod]
    public async Task EdgebandsGetEdgebandTypeInventoryHistoryAsync()
    {
        var materialManagerClient = GetMaterialManagerClient();
        await MaterialManagerReadEdgebandResultsSamples.Edgebands_GetEdgebandTypeInventoryHistoryAsync(materialManagerClient.Material.Edgebands);
    }
    
    //Edgebands_GetTechnologyMacrosFromMachine missing, no valid argument

    [TestMethod]
    public async Task EdgebandsGetLicensedMachines()
    {
        var materialManagerClient = GetMaterialManagerClient();
        await MaterialManagerReadEdgebandResultsSamples.Edgebands_GetLicensedMachines(materialManagerClient.Material.Edgebands);
    }

    [ClassCleanup]
    public async Task Cleanup()
    {
        var materialManagerClient = GetMaterialManagerClient();
        await materialManagerClient.Material.Edgebands.DeleteEdgebandType("ABS_Abruzzo_colore_1.00_100.0_HM");
    }

}