using System;
using OneClickDesktop.BackendClasses.Model;

namespace OneClickDesktop.BackendClasses.Communication.MessagesTemplates
{
    /// <summary>
    /// Describes common part of model report message sent from overseers to virtServers
    /// </summary>
    public class ModelReportTemplate
    {
        /// <summary>
        /// Message name - used to recognize package type
        /// </summary>
        public const string MessageTypeName = "ModelReport";

        /// <summary>
        /// Type of message body - used to properly cast after deserialization
        /// </summary>
        //const z jakiegos powodu ne dziala! typeof() poiwnno byc w compile-time ogolnie rsolvovane, ale nie jest(?).
        public static readonly Type MessageType = typeof(VirtualizationServer);

        /// <summary>
        /// Convert message body to correct type
        /// </summary>
        /// <param name="data">Message body</param>
        /// <returns>Message body as VirtualizationServer</returns>
        public static VirtualizationServer ConversionReceivedData(object data) => data as VirtualizationServer;
    }
}