using System.Net.Http.Headers;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Exceptions;
using HomagConnect.Base.Extensions;
using HomagConnect.Base.TestBase;
using HomagConnect.MaterialAssist.Client;
using HomagConnect.MaterialAssist.Contracts.Request;
using HomagConnect.MaterialManager.Client;
using HomagConnect.MaterialManager.Contracts.Material.Base;
using HomagConnect.MaterialManager.Contracts.Material.Boards;
using HomagConnect.MaterialManager.Contracts.Material.Boards.Enumerations;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands.Enumerations;
using HomagConnect.MaterialManager.Contracts.Request;

namespace HomagConnect.MaterialAssist.Tests
{
    /// <summary />
    public class MaterialAssistTestBase : TestBase
    {
        /// <summary />
        protected MaterialAssistClient GetMaterialAssistClient()
        {
            $"BaseUrl: {BaseUrl}, Subscription: {SubscriptionId}, AuthorizationKey: {AuthorizationKey[..4]}*".Trace();

            var httpClient = new HttpClient
            {
                BaseAddress = BaseUrl
            };

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", EncodeBase64Token(SubscriptionId.ToString(), AuthorizationKey));

            return new MaterialAssistClient(httpClient);
        }
        
        protected async Task EnsureBoardEntityExist(string id, string boardCode, ManagementType managementType = ManagementType.Single, Int32 quantity = 1)
        {
            var materialAssistClient = GetMaterialAssistClient();

            BoardEntity? boardEntity = null;

            try
            {
                boardEntity = await materialAssistClient.Boards.GetBoardEntityById(id);
            }
            catch (ProblemDetailsException ex)
            {
                if (ex.Message.Contains("No board entity found."))
                { 
                    throw;
                }
            }

            if (boardEntity == null)
            {
                await materialAssistClient.Boards.CreateBoardEntity(new MaterialAssistRequestBoardEntity
                {
                    Id = id,
                    BoardCode = boardCode,
                    ManagementType = managementType,
                    Quantity = quantity
                });
            }
        }

        protected async Task EnsureEdgebandEntityExist(string id, string edgebandCode, ManagementType managementType = ManagementType.Single, Int32 quantity = 1)
        {
            var materialAssistClient = GetMaterialAssistClient();

            EdgebandEntity? edgebandEntity = null;

            try
            {
                edgebandEntity = await materialAssistClient.Edgebands.GetEdgebandEntityById(id);
            }
            catch (ProblemDetailsException ex)
            {
                if (ex.Message.Contains("No edgeband entity found."))
                {
                    throw;
                }
            }

            if (edgebandEntity == null)
            {
                await materialAssistClient.Edgebands.CreateEdgebandEntity(new MaterialAssistRequestEdgebandEntity
                {
                    Id = id,
                    EdgebandCode = edgebandCode,
                    ManagementType = managementType,
                    Quantity = quantity
                });
            }
        }

        protected MaterialManagerClient GetMaterialManagerClient()
        {
            $"BaseUrl: {BaseUrl}, Subscription: {SubscriptionId}, AuthorizationKey: {AuthorizationKey[..4]}*".Trace();

            var httpClient = new HttpClient
            {
                BaseAddress = BaseUrl
            };

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", EncodeBase64Token(SubscriptionId.ToString(), AuthorizationKey));

            return new MaterialManagerClient(httpClient);
        }

        protected async Task EnsureBoardTypeExist(string materialCode, double length = 2800, double width = 2070, bool offcut = false)
        {
            string boardCode;
            BoardTypeType boardTypeType;

            if (offcut)
            {
                boardCode = $"X{materialCode}_{length}_{width}";
                boardTypeType = BoardTypeType.Offcut;
            }
            else
            {
                boardCode = $"{materialCode}_{length}_{width}";
                boardTypeType = BoardTypeType.Board;
            }

            var materialManagerClient = GetMaterialManagerClient();

            BoardType? boardType = null;

            try
            {
                boardType = await materialManagerClient.Material.Boards.GetBoardTypeByBoardCode(boardCode);
            }
            catch (ProblemDetailsException ex)
            {
                if (!ex.Message.Contains("No board types found."))
                {
                    throw;
                }
            }

            if (boardType == null)
            {
                await materialManagerClient.Material.Boards.CreateBoardType(new MaterialManagerRequestBoardType
                {
                    MaterialCode = materialCode,
                    BoardCode = boardCode,
                    Thickness = 19.0,
                    Grain = Grain.None,
                    Width = width,
                    Length = length,
                    Type = boardTypeType,
                    CoatingCategory = CoatingCategory.Undefined,
                    MaterialCategory = BoardMaterialCategory.Undefined
                });
            }
        }

        protected async Task EnsureEdgebandTypeExist(string edgebandCode, double thickness = 1.0, double length = 23.0)
        {
            var materialManagerClient = GetMaterialManagerClient();

            EdgebandType? edgebandType = null;

            try
            {
                edgebandType = await materialManagerClient.Material.Edgebands.GetEdgebandTypeByEdgebandCode(edgebandCode);
            }
            catch (ProblemDetailsException ex)
            {
                if (!ex.Message.Contains("No edgeband types found."))
                {
                    throw;
                }
            }

            if (edgebandType == null)
            {
                await materialManagerClient.Material.Edgebands.CreateEdgebandType(new MaterialManagerRequestEdgebandType
                {
                    EdgebandCode = edgebandCode,
                    Height = 20,
                    Thickness = thickness,
                    DefaultLength = length,
                    MaterialCategory = EdgebandMaterialCategory.ABS,
                    Process = EdgebandingProcess.Other,
                });
            }
        }
    }
}