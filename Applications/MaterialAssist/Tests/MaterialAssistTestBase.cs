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

        /*
        protected async Task EnsureBoardEntityExist(string id, string boardCode, ManagementType managementType = ManagementType.Single, Int32 quantity = 1)
        {
            var materialAssistClient = GetMaterialAssistClient();

            IList<BoardEntity> boardEntity;

            try
            {
                boardEntity = await materialAssistClient.Boards.GetBoardEntityById(new[] { id })
                    .ToListAsync();
            }
            catch (ProblemDetailsException ex)
            {
                if (ex.Message.Contains("No board entity found."))
                {
                    boardEntity = new List<BoardEntity>();
                }
                else
                {
                    throw;
                }
            }

            if (boardEntity.All(b => b.Id != id))
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

            IList<EdgebandEntity> edgebandEntity;

            try
            {
                edgebandEntity = await materialAssistClient.Edgebands.GetEdgebandEntityById(new[] { id })
                    .ToListAsync();
            }
            catch (ProblemDetailsException ex)
            {
                if (ex.Message.Contains("No edgeband entity found."))
                {
                    edgebandEntity = new List<EdgebandEntity>();
                }
                else
                {
                    throw;
                }
            }

            if (edgebandEntity.All(b => b.Id != id))
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
        */

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

        protected async Task EnsureBoardTypeExists(string materialCode, double length = 2800.0, double width = 2070.0)
        {
            var materialManagerClient = GetMaterialManagerClient();

            IList<BoardType> boardTypes;

            try
            {
                boardTypes = await materialManagerClient.Material.Boards.GetBoardTypesByMaterialCodes(new[] { materialCode })
                    .ToListAsync();
            }
            catch (ProblemDetailsException ex)
            {
                if (ex.Message.Contains("No board types found."))
                {
                    boardTypes = new List<BoardType>();
                }
                else
                {
                    throw;
                }
            }

            if (boardTypes.All(b => b.MaterialCode != materialCode))
            {
                await materialManagerClient.Material.Boards.CreateBoardType(new MaterialManagerRequestBoardType
                {
                    MaterialCode = materialCode,
                    BoardCode = $"{materialCode}_{length}_{width}",
                    Thickness = 19.0,
                    Grain = Grain.None,
                    Width = length,
                    Length = width,
                    Type = BoardTypeType.Board,
                    CoatingCategory = CoatingCategory.Undefined,
                    MaterialCategory = BoardMaterialCategory.Undefined
                });
            }
        }

        protected async Task EnsureEdgebandTypeExists(string edgebandCode, double thickness = 1.0, double length = 23.0)
        {
            var materialManagerClient = GetMaterialManagerClient();

            IList<EdgebandType> endgebandTypes;

            try
            {
                endgebandTypes = await materialManagerClient.Material.Edgebands.GetEdgebandTypesByEdgebandCodes(new[] { edgebandCode })
                    .ToListAsync();
            }
            catch (ProblemDetailsException ex)
            {
                if (ex.Message.Contains("No edgeband types found."))
                {
                    endgebandTypes = new List<EdgebandType>();
                }
                else
                {
                    throw;
                }
            }

            if (endgebandTypes.All(b => b.EdgebandCode != edgebandCode))
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