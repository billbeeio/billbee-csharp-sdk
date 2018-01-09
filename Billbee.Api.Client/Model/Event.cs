using System;
using System.Collections.Generic;
using System.Text;

namespace BillBee.API.Client.Model
{
    public class Event
    {
        /// <summary>
        /// Timestamp, when the even was created
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Id of the event type
        /// </summary>
        public int TypeId { get; set; }

        /// <summary>
        /// Readable text representation of event type
        /// </summary>
        public string TypeText { get; set; }

        /// <summary>
        /// Internal id of this event
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Internal id of the employee, that initiated this event, if applicable
        /// </summary>
        public int? EmployeeId { get; set; }

        /// <summary>
        /// Name of the employee, that initiated this event, if applicable
        /// </summary>
        public string EmployeeName { get; set; }

        /// <summary>
        /// Internal id of the order, this event is based on, if applicable
        /// </summary>
        public int? OrderId { get; set; }
    }
}
