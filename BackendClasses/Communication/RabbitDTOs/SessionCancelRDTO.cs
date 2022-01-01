using OneClickDesktop.BackendClasses.Model;
using OneClickDesktop.BackendClasses.Model.States;

namespace OneClickDesktop.BackendClasses.Communication.RabbitDTOs
{
    /// <summary>
    /// 
    /// </summary>
    public class SessionCancelRDTO
    {
        /// <summary>
        /// 
        /// </summary>
        public SessionState StateOnCancel { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Session CanceledSession { get; set; } 
    }
}