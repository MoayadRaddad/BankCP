﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WCFServicesApp.WCFServices {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="WCFServices.IWCFServices")]
    public interface IWCFServices {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWCFServices/getScreen", ReplyAction="http://tempuri.org/IWCFServices/getScreenResponse")]
        BusinessObjects.Models.Screen getScreen(string bankId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWCFServices/getScreen", ReplyAction="http://tempuri.org/IWCFServices/getScreenResponse")]
        System.Threading.Tasks.Task<BusinessObjects.Models.Screen> getScreenAsync(string bankId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWCFServices/getButtons", ReplyAction="http://tempuri.org/IWCFServices/getButtonsResponse")]
        BusinessObjects.Models.CustomIssueTicketAndShowMessageButtons getButtons(string bankId, string branchId, string screenId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWCFServices/getButtons", ReplyAction="http://tempuri.org/IWCFServices/getButtonsResponse")]
        System.Threading.Tasks.Task<BusinessObjects.Models.CustomIssueTicketAndShowMessageButtons> getButtonsAsync(string bankId, string branchId, string screenId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWCFServices/GetScreens", ReplyAction="http://tempuri.org/IWCFServices/GetScreensResponse")]
        BusinessObjects.Models.Screen[] GetScreens();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWCFServices/GetScreens", ReplyAction="http://tempuri.org/IWCFServices/GetScreensResponse")]
        System.Threading.Tasks.Task<BusinessObjects.Models.Screen[]> GetScreensAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IWCFServicesChannel : WCFServicesApp.WCFServices.IWCFServices, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class WCFServicesClient : System.ServiceModel.ClientBase<WCFServicesApp.WCFServices.IWCFServices>, WCFServicesApp.WCFServices.IWCFServices {
        
        public WCFServicesClient() {
        }
        
        public WCFServicesClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public WCFServicesClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WCFServicesClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WCFServicesClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public BusinessObjects.Models.Screen getScreen(string bankId) {
            return base.Channel.getScreen(bankId);
        }
        
        public System.Threading.Tasks.Task<BusinessObjects.Models.Screen> getScreenAsync(string bankId) {
            return base.Channel.getScreenAsync(bankId);
        }
        
        public BusinessObjects.Models.CustomIssueTicketAndShowMessageButtons getButtons(string bankId, string branchId, string screenId) {
            return base.Channel.getButtons(bankId, branchId, screenId);
        }
        
        public System.Threading.Tasks.Task<BusinessObjects.Models.CustomIssueTicketAndShowMessageButtons> getButtonsAsync(string bankId, string branchId, string screenId) {
            return base.Channel.getButtonsAsync(bankId, branchId, screenId);
        }
        
        public BusinessObjects.Models.Screen[] GetScreens() {
            return base.Channel.GetScreens();
        }
        
        public System.Threading.Tasks.Task<BusinessObjects.Models.Screen[]> GetScreensAsync() {
            return base.Channel.GetScreensAsync();
        }
    }
}
