After automatically generating code from WSDL via svcutil, the "ReplyAction" attribute must be changed from ="*" to =""
	<System.ServiceModel.OperationContractAttribute(Action:="GetIdentifyResults", ReplyAction:="*")
		to
	<System.ServiceModel.OperationContractAttribute(Action:="GetIdentifyResults", ReplyAction:="")