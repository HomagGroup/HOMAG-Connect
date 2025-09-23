using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Events;
using HomagConnect.MaterialManager.Contracts.Material.Boards;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace  HomagConnect.MaterialManager.Contracts.Events
{
    /// <summary>
    /// Gests or sets an event that occurs when a Board is confirmed to be used in productionAssist
    /// </summary>
    [AppEvent("FeedbackWorkstationNestingPlanBoardConfirmed")]
    public class FeedbackWorkstationNestingPlanBoardConfirmedEvent : AppEvent
    {

        /// <summary>
        /// The OptimizationId in which the board was used
        /// </summary>
        public Guid OptimizationId { get; set; }

        /// <summary>
        /// The OptimizationName in which the board was used
        /// </summary>
        public string OptimizationName { get; set; }

        /// <summary>
        /// The WorkplaceId from which the part was confirmed
        /// </summary>
        public Guid WorkstationId { get; set; }

        /// <summary>
        /// The WorkplaceName from which the part was confirmed
        /// </summary>
        public string Workstation { get; set; }

        /// <summary>
        /// The WorkplaceName from which the part was confirmed
        /// </summary>
        public WorkstationType WorkstationType => WorkstationType.Nesting;

        /// <summary>
        /// PatternCycle
        /// </summary>
        public int PatternCycle { get; set; }

        /// <summary>
        /// The pattern (i.e. 00001) for which the Board was confirmed
        /// </summary>
        public string PatternName { get; set; }

        /// <summary>
        /// The BoardType object
        /// </summary>
        public BoardType? BoardType { get; set; }

        /// <summary>
        /// The BoardEntity (Board information from material including the location in storage)
        /// </summary>
        public BoardEntity? BoardEntity { get; set; }
    }
}
