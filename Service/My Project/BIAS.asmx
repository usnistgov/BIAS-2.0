<?xml version="1.0" encoding="utf-8"?>
<wsdl:definitions xmlns:tm="http://microsoft.com/wsdl/mime/textMatching/" xmlns:soapenc="http://schemas.xmlsoap.org/soap/encoding/" xmlns:mime="http://schemas.xmlsoap.org/wsdl/mime/" xmlns:tns="http://docs.oasis-open.org/bias/ns/bias-2.0/" xmlns:soap="http://schemas.xmlsoap.org/wsdl/soap/" xmlns:s1="http://standards.iso.org/iso-iec/19785/-3/ed-2/" xmlns:s="http://www.w3.org/2001/XMLSchema" xmlns:soap12="http://schemas.xmlsoap.org/wsdl/soap12/" xmlns:http="http://schemas.xmlsoap.org/wsdl/http/" targetNamespace="http://docs.oasis-open.org/bias/ns/bias-2.0/" xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">
  <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">BIAS Tester Service</wsdl:documentation>
  <wsdl:types>
    <s:schema elementFormDefault="qualified" targetNamespace="http://docs.oasis-open.org/bias/ns/bias-2.0/">
      <s:import namespace="http://standards.iso.org/iso-iec/19785/-3/ed-2/" />
      <s:element name="HelloWorld">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="GreetingMessage" type="s:string" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="HelloWorldResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="HelloWorldResult" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="AddSubjectToGallery">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="AddSubjectToGalleryRequest" type="tns:AddSubjectToGalleryRequest" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:complexType name="AddSubjectToGalleryRequest">
        <s:complexContent mixed="false">
          <s:extension base="tns:RequestTemplate">
            <s:sequence>
              <s:element minOccurs="0" maxOccurs="1" name="GalleryID" type="s:string" />
              <s:element minOccurs="0" maxOccurs="1" name="Identity" type="tns:BIASIdentity" />
            </s:sequence>
          </s:extension>
        </s:complexContent>
      </s:complexType>
      <s:complexType name="RequestTemplate">
        <s:sequence>
          <s:element minOccurs="0" maxOccurs="1" name="ExtensionData" type="tns:ExtensionDataObject" />
          <s:element minOccurs="0" maxOccurs="1" name="GenericRequestParameters" type="tns:GenericRequestParameters" />
        </s:sequence>
      </s:complexType>
      <s:complexType name="ExtensionDataObject" />
      <s:complexType name="GenericRequestParameters">
        <s:sequence>
          <s:element minOccurs="0" maxOccurs="1" name="ExtensionData" type="tns:ExtensionDataObject" />
          <s:element minOccurs="0" maxOccurs="1" name="Application" type="s:string" />
          <s:element minOccurs="0" maxOccurs="1" name="ApplicationUser" type="s:string" />
          <s:element minOccurs="0" maxOccurs="1" name="BIASOperationName" type="s:string" />
        </s:sequence>
      </s:complexType>
      <s:complexType name="BIASIdentity">
        <s:sequence>
          <s:element minOccurs="0" maxOccurs="1" name="ExtensionData" type="tns:ExtensionDataObject" />
          <s:element minOccurs="0" maxOccurs="1" name="SubjectID" type="s:string" />
          <s:element minOccurs="0" maxOccurs="1" name="IdentityClaim" type="s:string" />
          <s:element minOccurs="0" maxOccurs="1" name="EncounterID" type="s:string" />
          <s:element minOccurs="0" maxOccurs="1" name="EncounterList" type="tns:ArrayOfString" />
          <s:element minOccurs="0" maxOccurs="1" name="BiographicData" type="tns:BiographicDataType" />
          <s:element minOccurs="0" maxOccurs="1" name="BiometricData" type="tns:BIASBiometricDataType" />
        </s:sequence>
      </s:complexType>
      <s:complexType name="ArrayOfString">
        <s:sequence>
          <s:element minOccurs="0" maxOccurs="unbounded" name="string" nillable="true" type="s:string" />
        </s:sequence>
      </s:complexType>
      <s:complexType name="BIASBiometricDataType">
        <s:sequence>
          <s:element minOccurs="0" maxOccurs="1" name="ExtensionData" type="tns:ExtensionDataObject" />
          <s:element minOccurs="0" maxOccurs="1" name="BIRList" type="tns:ArrayOfCBEFF_BIR_Type" />
          <s:element minOccurs="0" maxOccurs="1" name="BIR" type="tns:CBEFF_BIR_Type" />
          <s:element minOccurs="0" maxOccurs="1" name="InputBIR" type="tns:CBEFF_BIR_Type" />
          <s:element minOccurs="0" maxOccurs="1" name="ReferenceBIR" type="tns:CBEFF_BIR_Type" />
          <s:element minOccurs="0" maxOccurs="1" name="BiometricDataList" type="tns:ArrayOfBiometricDataType" />
        </s:sequence>
      </s:complexType>
      <s:complexType name="ArrayOfCBEFF_BIR_Type">
        <s:sequence>
          <s:element minOccurs="0" maxOccurs="unbounded" name="CBEFF_BIR_Type" nillable="true" type="tns:CBEFF_BIR_Type" />
        </s:sequence>
      </s:complexType>
      <s:complexType name="CBEFF_BIR_Type">
        <s:sequence>
          <s:element minOccurs="0" maxOccurs="1" name="ExtensionData" type="tns:ExtensionDataObject" />
          <s:element minOccurs="1" maxOccurs="1" name="FormatOwner" type="s:long" />
          <s:element minOccurs="1" maxOccurs="1" name="FormatType" type="s:long" />
          <s:element minOccurs="0" maxOccurs="1" name="BIR_Information" type="tns:BIR_InformationType" />
          <s:element minOccurs="0" maxOccurs="1" name="BIR" type="tns:BaseBIRType" />
        </s:sequence>
      </s:complexType>
      <s:complexType name="BIR_InformationType">
        <s:sequence>
          <s:element minOccurs="0" maxOccurs="1" name="ExtensionData" type="tns:ExtensionDataObject" />
          <s:element minOccurs="0" maxOccurs="1" name="BIR_Info" type="tns:BIRInfoType" />
          <s:element minOccurs="0" maxOccurs="1" name="BDB_Info" type="tns:BDBInfoType" />
          <s:element minOccurs="0" maxOccurs="1" name="SB_Info" type="tns:SBInfoType" />
        </s:sequence>
      </s:complexType>
      <s:complexType name="BIRInfoType">
        <s:sequence>
          <s:element minOccurs="0" maxOccurs="1" name="ExtensionData" type="tns:ExtensionDataObject" />
          <s:element minOccurs="0" maxOccurs="1" name="Creator" type="s:string" />
          <s:element minOccurs="0" maxOccurs="1" name="Index" type="s:string" />
          <s:element minOccurs="0" maxOccurs="1" name="Payload" type="s:base64Binary" />
          <s:element minOccurs="1" maxOccurs="1" name="Integrity" type="s:boolean" />
          <s:element minOccurs="1" maxOccurs="1" name="CreationDate" type="s:dateTime" />
          <s:element minOccurs="1" maxOccurs="1" name="NotValidBefore" type="s:dateTime" />
          <s:element minOccurs="1" maxOccurs="1" name="NotValidAfter" type="s:dateTime" />
        </s:sequence>
      </s:complexType>
      <s:complexType name="BDBInfoType">
        <s:sequence>
          <s:element minOccurs="0" maxOccurs="1" name="ExtensionData" type="tns:ExtensionDataObject" />
          <s:element minOccurs="0" maxOccurs="1" name="ChallengeResponse" type="s:base64Binary" />
          <s:element minOccurs="0" maxOccurs="1" name="Index" type="s:string" />
          <s:element minOccurs="0" maxOccurs="1" name="Format" type="tns:RegistryIDType" />
          <s:element minOccurs="1" maxOccurs="1" name="Encryption" type="s:boolean" />
          <s:element minOccurs="1" maxOccurs="1" name="CreationDate" type="s:dateTime" />
          <s:element minOccurs="1" maxOccurs="1" name="NotValidBefore" type="s:dateTime" />
          <s:element minOccurs="1" maxOccurs="1" name="NotValidAfter" type="s:dateTime" />
          <s:element minOccurs="0" maxOccurs="1" name="Type" type="s1:MultipleTypesType" />
          <s:element minOccurs="0" maxOccurs="1" name="Subtype" type="s1:SubtypeType" />
          <s:element minOccurs="1" maxOccurs="1" name="Level" type="tns:ProcessedLevelType" />
          <s:element minOccurs="0" maxOccurs="1" name="Product" type="tns:RegistryIDType" />
          <s:element minOccurs="0" maxOccurs="1" name="CaptureDevice" type="tns:RegistryIDType" />
          <s:element minOccurs="0" maxOccurs="1" name="FeatureExtractionAlgorithm" type="tns:RegistryIDType" />
          <s:element minOccurs="0" maxOccurs="1" name="ComparisonAlgorithm" type="tns:RegistryIDType" />
          <s:element minOccurs="0" maxOccurs="1" name="CompressionAlgorithm" type="tns:RegistryIDType" />
          <s:element minOccurs="1" maxOccurs="1" name="Purpose" type="tns:PurposeType" />
          <s:element minOccurs="0" maxOccurs="1" name="Quality" type="s1:QualityType" />
        </s:sequence>
      </s:complexType>
      <s:complexType name="RegistryIDType">
        <s:sequence>
          <s:element minOccurs="0" maxOccurs="1" name="ExtensionData" type="tns:ExtensionDataObject" />
          <s:element minOccurs="0" maxOccurs="1" name="Organization" type="s:string" />
          <s:element minOccurs="0" maxOccurs="1" name="Type" type="s:string" />
        </s:sequence>
      </s:complexType>
      <s:simpleType name="ProcessedLevelType">
        <s:restriction base="s:string">
          <s:enumeration value="Raw" />
          <s:enumeration value="Intermediate" />
          <s:enumeration value="Processed" />
        </s:restriction>
      </s:simpleType>
      <s:simpleType name="PurposeType">
        <s:restriction base="s:string">
          <s:enumeration value="Verify" />
          <s:enumeration value="Identify" />
          <s:enumeration value="Enroll" />
          <s:enumeration value="EnrollVerify" />
          <s:enumeration value="EnrollIdentify" />
          <s:enumeration value="Audit" />
        </s:restriction>
      </s:simpleType>
      <s:complexType name="SBInfoType">
        <s:sequence>
          <s:element minOccurs="0" maxOccurs="1" name="ExtensionData" type="tns:ExtensionDataObject" />
          <s:element minOccurs="0" maxOccurs="1" name="Format" type="tns:RegistryIDType" />
        </s:sequence>
      </s:complexType>
      <s:complexType name="BaseBIRType">
        <s:sequence>
          <s:element minOccurs="0" maxOccurs="1" name="ExtensionData" type="tns:ExtensionDataObject" />
        </s:sequence>
      </s:complexType>
      <s:complexType name="ArrayOfBiometricDataType">
        <s:sequence>
          <s:element minOccurs="0" maxOccurs="unbounded" name="BiometricDataType" nillable="true" type="tns:BiometricDataType" />
        </s:sequence>
      </s:complexType>
      <s:complexType name="BiometricDataType">
        <s:sequence>
          <s:element minOccurs="0" maxOccurs="1" name="ExtensionData" type="tns:ExtensionDataObject" />
          <s:element minOccurs="0" maxOccurs="1" name="BiometricType" type="s1:MultipleTypesType" />
          <s:element minOccurs="1" maxOccurs="1" name="BiometricTypeCount" type="s:long" />
          <s:element minOccurs="0" maxOccurs="1" name="BiometricSubType" type="s1:SubtypeType" />
          <s:element minOccurs="1" maxOccurs="1" name="BDBFormatOwner" type="s:long" />
          <s:element minOccurs="1" maxOccurs="1" name="BDBFormatType" type="s:long" />
        </s:sequence>
      </s:complexType>
      <s:complexType name="ResponseTemplate">
        <s:sequence>
          <s:element minOccurs="0" maxOccurs="1" name="ExtensionData" type="tns:ExtensionDataObject" />
          <s:element minOccurs="0" maxOccurs="1" name="ResponseStatus" type="tns:ResponseStatus" />
        </s:sequence>
      </s:complexType>
      <s:complexType name="ResponseStatus">
        <s:sequence>
          <s:element minOccurs="0" maxOccurs="1" name="ExtensionData" type="tns:ExtensionDataObject" />
          <s:element minOccurs="1" maxOccurs="1" name="Return" type="s:unsignedLong" />
          <s:element minOccurs="0" maxOccurs="1" name="Message" type="s:string" />
        </s:sequence>
      </s:complexType>
      <s:complexType name="AddSubjectToGalleryResponsePackage">
        <s:complexContent mixed="false">
          <s:extension base="tns:ResponseTemplate" />
        </s:complexContent>
      </s:complexType>
      <s:element name="AddSubjectToGalleryResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="AddSubjectToGalleryResult" type="tns:AddSubjectToGalleryResponsePackage" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="CheckQuality">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="CheckQualityRequest" type="tns:CheckQualityRequest" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:complexType name="CheckQualityRequest">
        <s:complexContent mixed="false">
          <s:extension base="tns:RequestTemplate">
            <s:sequence>
              <s:element minOccurs="0" maxOccurs="1" name="BiometricData" type="tns:BIASBiometricDataType" />
              <s:element minOccurs="0" maxOccurs="1" name="Identity" type="tns:BIASIdentity" />
              <s:element minOccurs="0" maxOccurs="1" name="QualityInfo" type="tns:QualityData" />
            </s:sequence>
          </s:extension>
        </s:complexContent>
      </s:complexType>
      <s:complexType name="QualityData">
        <s:sequence>
          <s:element minOccurs="0" maxOccurs="1" name="ExtensionData" type="tns:ExtensionDataObject" />
          <s:element minOccurs="0" maxOccurs="1" name="QualityScore" type="s1:QualityType" />
          <s:element minOccurs="0" maxOccurs="1" name="AlgorithmVendor" type="s:string" />
          <s:element minOccurs="0" maxOccurs="1" name="AlgorithmVendorProductID" type="s:string" />
          <s:element minOccurs="0" maxOccurs="1" name="AlgorithmVersion" type="s:string" />
        </s:sequence>
      </s:complexType>
      <s:element name="CheckQualityResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="CheckQualityResult" type="tns:CheckQualityResponsePackage" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:complexType name="CheckQualityResponsePackage">
        <s:complexContent mixed="false">
          <s:extension base="tns:ResponseTemplate">
            <s:sequence>
              <s:element minOccurs="0" maxOccurs="1" name="QualityInfo" type="tns:QualityData" />
            </s:sequence>
          </s:extension>
        </s:complexContent>
      </s:complexType>
      <s:element name="ClassifyBiometricData">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="ClassifyBiometricDataRequest" type="tns:ClassifyBiometricDataRequest" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:complexType name="ClassifyBiometricDataRequest">
        <s:complexContent mixed="false">
          <s:extension base="tns:RequestTemplate">
            <s:sequence>
              <s:element minOccurs="0" maxOccurs="1" name="BiometricData" type="tns:BIASBiometricDataType" />
              <s:element minOccurs="0" maxOccurs="1" name="ClassificationData" type="tns:ClassificationData" />
            </s:sequence>
          </s:extension>
        </s:complexContent>
      </s:complexType>
      <s:complexType name="ClassificationData">
        <s:sequence>
          <s:element minOccurs="0" maxOccurs="1" name="ExtensionData" type="tns:ExtensionDataObject" />
          <s:element minOccurs="0" maxOccurs="1" name="Classification" type="s:string" />
          <s:element minOccurs="0" maxOccurs="1" name="ClassificationAlgorithmType" type="s:string" />
        </s:sequence>
      </s:complexType>
      <s:element name="ClassifyBiometricDataResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="ClassifyBiometricDataResult" type="tns:ClassifyBiometricDataResponsePackage" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:complexType name="ClassifyBiometricDataResponsePackage">
        <s:complexContent mixed="false">
          <s:extension base="tns:ResponseTemplate">
            <s:sequence>
              <s:element minOccurs="0" maxOccurs="1" name="ClassificationData" type="tns:ClassificationData" />
            </s:sequence>
          </s:extension>
        </s:complexContent>
      </s:complexType>
      <s:element name="CreateSubject">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="CreateSubjectRequest" type="tns:CreateSubjectRequest" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:complexType name="CreateSubjectRequest">
        <s:complexContent mixed="false">
          <s:extension base="tns:RequestTemplate" />
        </s:complexContent>
      </s:complexType>
      <s:element name="CreateSubjectResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="CreateSubjectResult" type="tns:CreateSubjectResponsePackage" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:complexType name="CreateSubjectResponsePackage">
        <s:complexContent mixed="false">
          <s:extension base="tns:ResponseTemplate">
            <s:sequence>
              <s:element minOccurs="0" maxOccurs="1" name="Identity" type="tns:BIASIdentity" />
            </s:sequence>
          </s:extension>
        </s:complexContent>
      </s:complexType>
      <s:element name="DeleteBiographicData">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="DeleteBiographicDataRequest" type="tns:DeleteBiographicDataRequest" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:complexType name="DeleteBiographicDataRequest">
        <s:complexContent mixed="false">
          <s:extension base="tns:RequestTemplate">
            <s:sequence>
              <s:element minOccurs="0" maxOccurs="1" name="Identity" type="tns:BIASIdentity" />
              <s:element minOccurs="0" maxOccurs="1" name="GalleryID" type="s:string" />
            </s:sequence>
          </s:extension>
        </s:complexContent>
      </s:complexType>
      <s:element name="DeleteBiographicDataResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="DeleteBiographicDataResult" type="tns:DeleteBiographicDataResponsePackage" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:complexType name="DeleteBiographicDataResponsePackage">
        <s:complexContent mixed="false">
          <s:extension base="tns:ResponseTemplate" />
        </s:complexContent>
      </s:complexType>
      <s:element name="DeleteBiometricData">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="DeleteBiometricDataRequest" type="tns:DeleteBiometricDataRequest" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:complexType name="DeleteBiometricDataRequest">
        <s:complexContent mixed="false">
          <s:extension base="tns:RequestTemplate">
            <s:sequence>
              <s:element minOccurs="0" maxOccurs="1" name="Identity" type="tns:BIASIdentity" />
              <s:element minOccurs="0" maxOccurs="1" name="BiometricType" type="s1:MultipleTypesType" />
              <s:element minOccurs="0" maxOccurs="1" name="GalleryID" type="s:string" />
            </s:sequence>
          </s:extension>
        </s:complexContent>
      </s:complexType>
      <s:element name="DeleteBiometricDataResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="DeleteBiometricDataResult" type="tns:DeleteBiometricDataResponsePackage" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:complexType name="DeleteBiometricDataResponsePackage">
        <s:complexContent mixed="false">
          <s:extension base="tns:ResponseTemplate" />
        </s:complexContent>
      </s:complexType>
      <s:element name="DeleteSubject">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="DeleteSubjectRequest" type="tns:DeleteSubjectRequest" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:complexType name="DeleteSubjectRequest">
        <s:complexContent mixed="false">
          <s:extension base="tns:RequestTemplate">
            <s:sequence>
              <s:element minOccurs="0" maxOccurs="1" name="Identity" type="tns:BIASIdentity" />
            </s:sequence>
          </s:extension>
        </s:complexContent>
      </s:complexType>
      <s:element name="DeleteSubjectResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="DeleteSubjectResult" type="tns:DeleteSubjectResponsePackage" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:complexType name="DeleteSubjectResponsePackage">
        <s:complexContent mixed="false">
          <s:extension base="tns:ResponseTemplate" />
        </s:complexContent>
      </s:complexType>
      <s:element name="DeleteSubjectFromGallery">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="DeleteSubjectFromGalleryRequest" type="tns:DeleteSubjectFromGalleryRequest" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:complexType name="DeleteSubjectFromGalleryRequest">
        <s:complexContent mixed="false">
          <s:extension base="tns:RequestTemplate">
            <s:sequence>
              <s:element minOccurs="0" maxOccurs="1" name="GalleryID" type="s:string" />
              <s:element minOccurs="0" maxOccurs="1" name="Identity" type="tns:BIASIdentity" />
            </s:sequence>
          </s:extension>
        </s:complexContent>
      </s:complexType>
      <s:element name="DeleteSubjectFromGalleryResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="DeleteSubjectFromGalleryResult" type="tns:DeleteSubjectFromGalleryResponsePackage" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:complexType name="DeleteSubjectFromGalleryResponsePackage">
        <s:complexContent mixed="false">
          <s:extension base="tns:ResponseTemplate" />
        </s:complexContent>
      </s:complexType>
      <s:element name="Enroll">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="EnrollRequest" type="tns:EnrollRequest" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:complexType name="EnrollRequest">
        <s:complexContent mixed="false">
          <s:extension base="tns:AggregateRequestTemplate">
            <s:sequence>
              <s:element minOccurs="0" maxOccurs="1" name="Identity" type="tns:BIASIdentity" />
            </s:sequence>
          </s:extension>
        </s:complexContent>
      </s:complexType>
      <s:complexType name="AggregateRequestTemplate">
        <s:complexContent mixed="false">
          <s:extension base="tns:RequestTemplate">
            <s:sequence>
              <s:element minOccurs="0" maxOccurs="1" name="ProcessingOptions" type="tns:ArrayOfOptionType" />
              <s:element minOccurs="0" maxOccurs="1" name="InputData" type="tns:InformationType" />
            </s:sequence>
          </s:extension>
        </s:complexContent>
      </s:complexType>
      <s:complexType name="ArrayOfOptionType">
        <s:sequence>
          <s:element minOccurs="0" maxOccurs="unbounded" name="OptionType" nillable="true" type="tns:OptionType" />
        </s:sequence>
      </s:complexType>
      <s:complexType name="OptionType">
        <s:sequence>
          <s:element minOccurs="0" maxOccurs="1" name="ExtensionData" type="tns:ExtensionDataObject" />
          <s:element minOccurs="0" maxOccurs="1" name="Key" type="s:string" />
          <s:element minOccurs="0" maxOccurs="1" name="Value" type="s:string" />
        </s:sequence>
      </s:complexType>
      <s:complexType name="InformationType">
        <s:sequence>
          <s:element minOccurs="0" maxOccurs="1" name="ExtensionData" type="tns:ExtensionDataObject" />
          <s:element minOccurs="0" maxOccurs="1" name="GUID" type="s:string" />
          <s:element minOccurs="0" maxOccurs="1" name="GivenName" type="s:string" />
          <s:element minOccurs="0" maxOccurs="1" name="FamilyName" type="s:string" />
          <s:element minOccurs="0" maxOccurs="1" name="DateOfBirth" type="s:string" />
          <s:element minOccurs="0" maxOccurs="1" name="Sex" type="s:string" />
          <s:element minOccurs="0" maxOccurs="1" name="Citizenship" type="s:string" />
          <s:element minOccurs="0" maxOccurs="1" name="Images" type="tns:ArrayOfImage" />
          <s:element minOccurs="0" maxOccurs="1" name="Identities" type="tns:ArrayOfString" />
        </s:sequence>
      </s:complexType>
      <s:complexType name="ArrayOfImage">
        <s:sequence>
          <s:element minOccurs="0" maxOccurs="unbounded" name="Image" nillable="true" type="tns:Image" />
        </s:sequence>
      </s:complexType>
      <s:complexType name="Image">
        <s:sequence>
          <s:element minOccurs="0" maxOccurs="1" name="ExtensionData" type="tns:ExtensionDataObject" />
          <s:element minOccurs="0" maxOccurs="1" name="ContentType" type="s:string" />
          <s:element minOccurs="0" maxOccurs="1" name="SkinTone" type="s:string" />
          <s:element minOccurs="0" maxOccurs="1" name="RightEyeColor" type="s:string" />
          <s:element minOccurs="0" maxOccurs="1" name="LeftEyeColor" type="s:string" />
          <s:element minOccurs="0" maxOccurs="1" name="EyeDistance" type="s:string" />
          <s:element minOccurs="0" maxOccurs="1" name="BridgeWidth" type="s:string" />
          <s:element minOccurs="0" maxOccurs="1" name="NoseTipWidth" type="s:string" />
          <s:element minOccurs="0" maxOccurs="1" name="NostrilWidth" type="s:string" />
          <s:element minOccurs="0" maxOccurs="1" name="LipWidth" type="s:string" />
          <s:element minOccurs="0" maxOccurs="1" name="LipHeight" type="s:string" />
          <s:element minOccurs="0" maxOccurs="1" name="JawLength" type="s:string" />
          <s:element minOccurs="0" maxOccurs="1" name="ImageData" type="s:base64Binary" />
        </s:sequence>
      </s:complexType>
      <s:complexType name="TokenType">
        <s:sequence>
          <s:element minOccurs="0" maxOccurs="1" name="ExtensionData" type="tns:ExtensionDataObject" />
          <s:element minOccurs="0" maxOccurs="1" name="TokenValue" type="s:string" />
          <s:element minOccurs="0" maxOccurs="1" name="Expiration" type="s:string" />
        </s:sequence>
      </s:complexType>
      <s:element name="EnrollResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="EnrollResult" type="tns:EnrollResponsePackage" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:complexType name="EnrollResponsePackage">
        <s:complexContent mixed="false">
          <s:extension base="tns:AggregateResponseTemplate">
            <s:sequence>
              <s:element minOccurs="0" maxOccurs="1" name="Identity" type="tns:BIASIdentity" />
              <s:element minOccurs="0" maxOccurs="1" name="Token" type="tns:TokenType" />
            </s:sequence>
          </s:extension>
        </s:complexContent>
      </s:complexType>
      <s:complexType name="AggregateResponseTemplate">
        <s:complexContent mixed="false">
          <s:extension base="tns:ResponseTemplate">
            <s:sequence>
              <s:element minOccurs="0" maxOccurs="1" name="ReturnData" type="tns:InformationType" />
            </s:sequence>
          </s:extension>
        </s:complexContent>
      </s:complexType>
      <s:element name="GetEnrollResults">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="GetEnrollResultsRequest" type="tns:GetEnrollResultsRequest" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:complexType name="GetEnrollResultsRequest">
        <s:complexContent mixed="false">
          <s:extension base="tns:RequestTemplate">
            <s:sequence>
              <s:element minOccurs="0" maxOccurs="1" name="Token" type="tns:TokenType" />
            </s:sequence>
          </s:extension>
        </s:complexContent>
      </s:complexType>
      <s:element name="GetEnrollResultsResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="GetEnrollResultsResult" type="tns:GetEnrollResultsResponsePackage" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:complexType name="GetEnrollResultsResponsePackage">
        <s:complexContent mixed="false">
          <s:extension base="tns:AggregateResponseTemplate">
            <s:sequence>
              <s:element minOccurs="0" maxOccurs="1" name="Identity" type="tns:BIASIdentity" />
            </s:sequence>
          </s:extension>
        </s:complexContent>
      </s:complexType>
      <s:element name="GetIdentifyResults">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="GetIdentifyResultsRequest" type="tns:GetIdentifyResultsRequest" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:complexType name="GetIdentifyResultsRequest">
        <s:complexContent mixed="false">
          <s:extension base="tns:RequestTemplate">
            <s:sequence>
              <s:element minOccurs="0" maxOccurs="1" name="Token" type="tns:TokenType" />
            </s:sequence>
          </s:extension>
        </s:complexContent>
      </s:complexType>
      <s:complexType name="CandidateType">
        <s:sequence>
          <s:element minOccurs="0" maxOccurs="1" name="ExtensionData" type="tns:ExtensionDataObject" />
          <s:element minOccurs="0" maxOccurs="1" name="ScoreList" type="tns:ScoreListType" />
          <s:element minOccurs="0" maxOccurs="1" name="Identity" type="tns:BIASIdentity" />
          <s:element minOccurs="1" maxOccurs="1" name="Rank" type="s:long" />
          <s:element minOccurs="0" maxOccurs="1" name="BiographicData" type="tns:BiographicDataType" />
        </s:sequence>
      </s:complexType>
      <s:complexType name="ScoreListType">
        <s:sequence>
          <s:element minOccurs="0" maxOccurs="1" name="ExtensionData" type="tns:ExtensionDataObject" />
          <s:element minOccurs="0" maxOccurs="1" name="Score" type="tns:ScoreType" />
        </s:sequence>
      </s:complexType>
      <s:complexType name="ScoreType">
        <s:sequence>
          <s:element minOccurs="0" maxOccurs="1" name="ExtensionData" type="tns:ExtensionDataObject" />
          <s:element minOccurs="1" maxOccurs="1" name="Value" type="s:float" />
          <s:element minOccurs="0" maxOccurs="1" name="BiometricType" type="s1:MultipleTypesType" />
          <s:element minOccurs="0" maxOccurs="1" name="BiometricSubType" type="s1:SubtypeType" />
        </s:sequence>
      </s:complexType>
      <s:complexType name="ArrayOfCandidateType">
        <s:sequence>
          <s:element minOccurs="0" maxOccurs="unbounded" name="CandidateType" nillable="true" type="tns:CandidateType" />
        </s:sequence>
      </s:complexType>
      <s:element name="GetIdentifyResultsResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="GetIdentifyResultsResult" type="tns:GetIdentifyResultsResponsePackage" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:complexType name="GetIdentifyResultsResponsePackage">
        <s:complexContent mixed="false">
          <s:extension base="tns:AggregateResponseTemplate">
            <s:sequence>
              <s:element minOccurs="0" maxOccurs="1" name="Identity" type="tns:BIASIdentity" />
              <s:element minOccurs="0" maxOccurs="1" name="CandidateList" type="tns:ArrayOfCandidateType" />
            </s:sequence>
          </s:extension>
        </s:complexContent>
      </s:complexType>
      <s:element name="GetIdentifySubjectResults">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="GetIdentifySubjectResultsRequest" type="tns:GetIdentifySubjectResultsRequest" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:complexType name="GetIdentifySubjectResultsRequest">
        <s:complexContent mixed="false">
          <s:extension base="tns:RequestTemplate">
            <s:sequence>
              <s:element minOccurs="0" maxOccurs="1" name="Token" type="tns:TokenType" />
            </s:sequence>
          </s:extension>
        </s:complexContent>
      </s:complexType>
      <s:element name="GetIdentifySubjectResultsResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="GetIdentifySubjectResultsResult" type="tns:GetIdentifySubjectResultsResponsePackage" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:complexType name="GetIdentifySubjectResultsResponsePackage">
        <s:complexContent mixed="false">
          <s:extension base="tns:ResponseTemplate">
            <s:sequence>
              <s:element minOccurs="0" maxOccurs="1" name="CandidateList" type="tns:ArrayOfCandidateType" />
            </s:sequence>
          </s:extension>
        </s:complexContent>
      </s:complexType>
      <s:element name="GetVerifyResults">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="GetVerifyResultsRequest" type="tns:GetVerifyResultsRequest" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:complexType name="GetVerifyResultsRequest">
        <s:complexContent mixed="false">
          <s:extension base="tns:RequestTemplate">
            <s:sequence>
              <s:element minOccurs="0" maxOccurs="1" name="Token" type="tns:TokenType" />
            </s:sequence>
          </s:extension>
        </s:complexContent>
      </s:complexType>
      <s:element name="GetVerifyResultsResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="GetVerifyResultsResult" type="tns:GetVerifyResultsResponsePackage" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:complexType name="GetVerifyResultsResponsePackage">
        <s:complexContent mixed="false">
          <s:extension base="tns:AggregateResponseTemplate">
            <s:sequence>
              <s:element minOccurs="1" maxOccurs="1" name="Match" type="s:boolean" />
              <s:element minOccurs="0" maxOccurs="1" name="Score" type="tns:ScoreType" />
              <s:element minOccurs="0" maxOccurs="1" name="Identity" type="tns:BIASIdentity" />
            </s:sequence>
          </s:extension>
        </s:complexContent>
      </s:complexType>
      <s:element name="Identify">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="IdentifyRequest" type="tns:IdentifyRequest" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:complexType name="IdentifyRequest">
        <s:complexContent mixed="false">
          <s:extension base="tns:AggregateRequestTemplate">
            <s:sequence>
              <s:element minOccurs="0" maxOccurs="1" name="GalleryID" type="s:string" />
              <s:element minOccurs="1" maxOccurs="1" name="MaxListSize" type="s:long" />
            </s:sequence>
          </s:extension>
        </s:complexContent>
      </s:complexType>
      <s:element name="IdentifyResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="IdentifyResult" type="tns:IdentifyResponsePackage" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:complexType name="IdentifyResponsePackage">
        <s:complexContent mixed="false">
          <s:extension base="tns:AggregateResponseTemplate">
            <s:sequence>
              <s:element minOccurs="0" maxOccurs="1" name="Identity" type="tns:BIASIdentity" />
              <s:element minOccurs="0" maxOccurs="1" name="CandidateList" type="tns:ArrayOfCandidateType" />
              <s:element minOccurs="0" maxOccurs="1" name="Token" type="tns:TokenType" />
            </s:sequence>
          </s:extension>
        </s:complexContent>
      </s:complexType>
      <s:element name="IdentifySubject">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="IdentifySubjectRequest" type="tns:IdentifySubjectRequest" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:complexType name="IdentifySubjectRequest">
        <s:complexContent mixed="false">
          <s:extension base="tns:RequestTemplate">
            <s:sequence>
              <s:element minOccurs="0" maxOccurs="1" name="GalleryID" type="s:string" />
              <s:element minOccurs="0" maxOccurs="1" name="Gallery" type="tns:ArrayOfCandidateType" />
              <s:element minOccurs="0" maxOccurs="1" name="Identity" type="tns:BIASIdentity" />
              <s:element minOccurs="1" maxOccurs="1" name="MaxListSize" type="s:long" />
            </s:sequence>
          </s:extension>
        </s:complexContent>
      </s:complexType>
      <s:complexType name="IdentifySubjectResultType">
        <s:sequence>
          <s:element minOccurs="0" maxOccurs="1" name="ExtensionData" type="tns:ExtensionDataObject" />
        </s:sequence>
      </s:complexType>
      <s:element name="IdentifySubjectResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="IdentifySubjectResult" type="tns:IdentifySubjectResponsePackage" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:complexType name="IdentifySubjectResponsePackage">
        <s:complexContent mixed="false">
          <s:extension base="tns:ResponseTemplate">
            <s:sequence>
              <s:element minOccurs="0" maxOccurs="1" name="IdentifySubjectResult" type="tns:IdentifySubjectResultType" />
            </s:sequence>
          </s:extension>
        </s:complexContent>
      </s:complexType>
      <s:element name="ListBiographicData">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="ListBiographicDataRequest" type="tns:ListBiographicDataRequest" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:complexType name="ListBiographicDataRequest">
        <s:complexContent mixed="false">
          <s:extension base="tns:RequestTemplate">
            <s:sequence>
              <s:element minOccurs="0" maxOccurs="1" name="Identity" type="tns:BIASIdentity" />
              <s:element minOccurs="0" maxOccurs="1" name="EncounterType" type="s:string" />
            </s:sequence>
          </s:extension>
        </s:complexContent>
      </s:complexType>
      <s:element name="ListBiographicDataResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="ListBiographicDataResult" type="tns:ListBiographicDataResponsePackage" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:complexType name="ListBiographicDataResponsePackage">
        <s:complexContent mixed="false">
          <s:extension base="tns:ResponseTemplate">
            <s:sequence>
              <s:element minOccurs="0" maxOccurs="1" name="Identity" type="tns:BIASIdentity" />
            </s:sequence>
          </s:extension>
        </s:complexContent>
      </s:complexType>
      <s:element name="ListBiometricData">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="ListBiometricDataRequest" type="tns:ListBiometricDataRequest" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:complexType name="ListBiometricDataRequest">
        <s:complexContent mixed="false">
          <s:extension base="tns:RequestTemplate">
            <s:sequence>
              <s:element minOccurs="0" maxOccurs="1" name="Identity" type="tns:BIASIdentity" />
              <s:element minOccurs="0" maxOccurs="1" name="EncounterType" type="s:string" />
              <s:element minOccurs="0" maxOccurs="1" name="ListFilter" type="tns:ListFilterType" />
            </s:sequence>
          </s:extension>
        </s:complexContent>
      </s:complexType>
      <s:complexType name="ListFilterType">
        <s:sequence>
          <s:element minOccurs="0" maxOccurs="1" name="ExtensionData" type="tns:ExtensionDataObject" />
          <s:element minOccurs="0" maxOccurs="1" name="BiometricTypeFilters" type="tns:ArrayOfMultipleTypesType" />
          <s:element minOccurs="1" maxOccurs="1" name="IncludeBiometricSubtype" type="s:boolean" />
        </s:sequence>
      </s:complexType>
      <s:complexType name="ArrayOfMultipleTypesType">
        <s:sequence>
          <s:element minOccurs="0" maxOccurs="unbounded" name="MultipleTypesType" nillable="true" type="s1:MultipleTypesType" />
        </s:sequence>
      </s:complexType>
      <s:element name="ListBiometricDataResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="ListBiometricDataResult" type="tns:ListBiometricDataResponsePackage" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:complexType name="ListBiometricDataResponsePackage">
        <s:complexContent mixed="false">
          <s:extension base="tns:ResponseTemplate">
            <s:sequence>
              <s:element minOccurs="0" maxOccurs="1" name="Identity" type="tns:BIASIdentity" />
            </s:sequence>
          </s:extension>
        </s:complexContent>
      </s:complexType>
      <s:element name="PerformFusion">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="PerformFusionRequest" type="tns:PerformFusionRequest" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:complexType name="PerformFusionRequest">
        <s:complexContent mixed="false">
          <s:extension base="tns:RequestTemplate">
            <s:sequence>
              <s:element minOccurs="0" maxOccurs="1" name="FusionInput" type="tns:ArrayOfArrayOfFusionInformationType" />
            </s:sequence>
          </s:extension>
        </s:complexContent>
      </s:complexType>
      <s:complexType name="ArrayOfArrayOfFusionInformationType">
        <s:sequence>
          <s:element minOccurs="0" maxOccurs="unbounded" name="ArrayOfFusionInformationType" nillable="true" type="tns:ArrayOfFusionInformationType" />
        </s:sequence>
      </s:complexType>
      <s:complexType name="ArrayOfFusionInformationType">
        <s:sequence>
          <s:element minOccurs="0" maxOccurs="unbounded" name="FusionInformationType" nillable="true" type="tns:FusionInformationType" />
        </s:sequence>
      </s:complexType>
      <s:complexType name="FusionInformationType">
        <s:sequence>
          <s:element minOccurs="0" maxOccurs="1" name="ExtensionData" type="tns:ExtensionDataObject" />
          <s:element minOccurs="0" maxOccurs="1" name="BiometricType" type="s1:MultipleTypesType" />
          <s:element minOccurs="0" maxOccurs="1" name="BiometricSubType" type="s1:SubtypeType" />
          <s:element minOccurs="0" maxOccurs="1" name="AlgorithmOwner" type="s:string" />
          <s:element minOccurs="0" maxOccurs="1" name="AlgorithmType" type="s:string" />
          <s:element minOccurs="0" maxOccurs="1" name="FusionResult" type="tns:FusionResult" />
        </s:sequence>
      </s:complexType>
      <s:complexType name="FusionResult">
        <s:sequence>
          <s:element minOccurs="0" maxOccurs="1" name="ExtensionData" type="tns:ExtensionDataObject" />
        </s:sequence>
      </s:complexType>
      <s:element name="PerformFusionResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="PerformFusionResult" type="tns:PerformFusionResponsePackage" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:complexType name="PerformFusionResponsePackage">
        <s:complexContent mixed="false">
          <s:extension base="tns:ResponseTemplate">
            <s:sequence>
              <s:element minOccurs="1" maxOccurs="1" name="Match" type="s:boolean" />
            </s:sequence>
          </s:extension>
        </s:complexContent>
      </s:complexType>
      <s:element name="QueryCapabilities">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="QueryCapabilitiesRequest" type="tns:QueryCapabilitiesRequest" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:complexType name="QueryCapabilitiesRequest">
        <s:complexContent mixed="false">
          <s:extension base="tns:RequestTemplate" />
        </s:complexContent>
      </s:complexType>
      <s:complexType name="CapabilityType">
        <s:sequence>
          <s:element minOccurs="0" maxOccurs="1" name="ExtensionData" type="tns:ExtensionDataObject" />
          <s:element minOccurs="1" maxOccurs="1" name="CapabilityName" type="tns:CapabilityName" />
          <s:element minOccurs="0" maxOccurs="1" name="CapabilityID" type="s:string" />
          <s:element minOccurs="0" maxOccurs="1" name="CapabilityDescription" type="s:string" />
          <s:element minOccurs="0" maxOccurs="1" name="CapabilityValue" type="s:string" />
          <s:element minOccurs="0" maxOccurs="1" name="CapabilitySupportingValue" type="s:string" />
          <s:element minOccurs="0" maxOccurs="1" name="CapabilityAdditionalInfo" type="s:string" />
        </s:sequence>
      </s:complexType>
      <s:simpleType name="CapabilityName">
        <s:restriction base="s:string">
          <s:enumeration value="AggregateInputDataOptional" />
          <s:enumeration value="AggregateInputDataRequired" />
          <s:enumeration value="AggregateProcessingOption" />
          <s:enumeration value="AggregateReturnData" />
          <s:enumeration value="AggregateServiceDescription" />
          <s:enumeration value="BiographicDataSet" />
          <s:enumeration value="CBEFFPatronFormat" />
          <s:enumeration value="ClassificationAlgorithmType" />
          <s:enumeration value="ConformanceClass" />
          <s:enumeration value="Gallery" />
          <s:enumeration value="IdentityModel" />
          <s:enumeration value="MatchAlgorithm" />
          <s:enumeration value="MatchScore" />
          <s:enumeration value="QualityAlgorithm" />
          <s:enumeration value="SupportedBiometric" />
          <s:enumeration value="TransformOperation" />
        </s:restriction>
      </s:simpleType>
      <s:complexType name="ArrayOfCapabilityType">
        <s:sequence>
          <s:element minOccurs="0" maxOccurs="unbounded" name="CapabilityType" nillable="true" type="tns:CapabilityType" />
        </s:sequence>
      </s:complexType>
      <s:element name="QueryCapabilitiesResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="QueryCapabilitiesResult" type="tns:QueryCapabilitiesResponsePackage" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:complexType name="QueryCapabilitiesResponsePackage">
        <s:complexContent mixed="false">
          <s:extension base="tns:ResponseTemplate">
            <s:sequence>
              <s:element minOccurs="0" maxOccurs="1" name="CapabilityList" type="tns:ArrayOfCapabilityType" />
            </s:sequence>
          </s:extension>
        </s:complexContent>
      </s:complexType>
      <s:element name="RetrieveBiographicData">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="RetrieveBiographicDataRequest" type="tns:RetrieveBiographicDataRequest" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:complexType name="RetrieveBiographicDataRequest">
        <s:complexContent mixed="false">
          <s:extension base="tns:RequestTemplate">
            <s:sequence>
              <s:element minOccurs="0" maxOccurs="1" name="Identity" type="tns:BIASIdentity" />
              <s:element minOccurs="0" maxOccurs="1" name="EncounterType" type="s:string" />
              <s:element minOccurs="0" maxOccurs="1" name="GalleryID" type="s:string" />
            </s:sequence>
          </s:extension>
        </s:complexContent>
      </s:complexType>
      <s:element name="RetrieveBiographicDataResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="RetrieveBiographicDataResult" type="tns:RetrieveBiographicDataResponsePackage" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:complexType name="RetrieveBiographicDataResponsePackage">
        <s:complexContent mixed="false">
          <s:extension base="tns:ResponseTemplate">
            <s:sequence>
              <s:element minOccurs="0" maxOccurs="1" name="Identity" type="tns:BIASIdentity" />
            </s:sequence>
          </s:extension>
        </s:complexContent>
      </s:complexType>
      <s:element name="RetrieveBiometricData">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="RetrieveBiometricDataRequest" type="tns:RetrieveBiometricDataRequest" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:complexType name="RetrieveBiometricDataRequest">
        <s:complexContent mixed="false">
          <s:extension base="tns:RequestTemplate">
            <s:sequence>
              <s:element minOccurs="0" maxOccurs="1" name="Identity" type="tns:BIASIdentity" />
              <s:element minOccurs="0" maxOccurs="1" name="EncounterType" type="s:string" />
              <s:element minOccurs="0" maxOccurs="1" name="GalleryID" type="s:string" />
              <s:element minOccurs="0" maxOccurs="1" name="BiometricType" type="s1:MultipleTypesType" />
            </s:sequence>
          </s:extension>
        </s:complexContent>
      </s:complexType>
      <s:element name="RetrieveBiometricDataResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="RetrieveBiometricDataResult" type="tns:RetrieveBiometricDataResponsePackage" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:complexType name="RetrieveBiometricDataResponsePackage">
        <s:complexContent mixed="false">
          <s:extension base="tns:ResponseTemplate">
            <s:sequence>
              <s:element minOccurs="0" maxOccurs="1" name="Identity" type="tns:BIASIdentity" />
            </s:sequence>
          </s:extension>
        </s:complexContent>
      </s:complexType>
      <s:element name="RetrieveData">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="RetrieveDataRequest" type="tns:RetrieveDataRequest" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:complexType name="RetrieveDataRequest">
        <s:complexContent mixed="false">
          <s:extension base="tns:RequestTemplate">
            <s:sequence>
              <s:element minOccurs="0" maxOccurs="1" name="ProcessingOptions" type="tns:ArrayOfOptionType" />
              <s:element minOccurs="0" maxOccurs="1" name="Identity" type="tns:BIASIdentity" />
            </s:sequence>
          </s:extension>
        </s:complexContent>
      </s:complexType>
      <s:element name="RetrieveDataResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="RetrieveDataResult" type="tns:RetrieveDataResponsePackage" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:complexType name="RetrieveDataResponsePackage">
        <s:complexContent mixed="false">
          <s:extension base="tns:AggregateResponseTemplate" />
        </s:complexContent>
      </s:complexType>
      <s:element name="SetBiographicData">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="SetBiographicDataRequest" type="tns:SetBiographicDataRequest" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:complexType name="SetBiographicDataRequest">
        <s:complexContent mixed="false">
          <s:extension base="tns:RequestTemplate">
            <s:sequence>
              <s:element minOccurs="0" maxOccurs="1" name="Identity" type="tns:BIASIdentity" />
              <s:element minOccurs="0" maxOccurs="1" name="GalleryID" type="s:string" />
            </s:sequence>
          </s:extension>
        </s:complexContent>
      </s:complexType>
      <s:element name="SetBiographicDataResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="SetBiographicDataResult" type="tns:SetBiographicDataResponsePackage" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:complexType name="SetBiographicDataResponsePackage">
        <s:complexContent mixed="false">
          <s:extension base="tns:ResponseTemplate">
            <s:sequence>
              <s:element minOccurs="0" maxOccurs="1" name="Identity" type="tns:BIASIdentity" />
            </s:sequence>
          </s:extension>
        </s:complexContent>
      </s:complexType>
      <s:element name="SetBiometricData">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="SetBiometricDataRequest" type="tns:SetBiometricDataRequest" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:complexType name="SetBiometricDataRequest">
        <s:complexContent mixed="false">
          <s:extension base="tns:RequestTemplate">
            <s:sequence>
              <s:element minOccurs="0" maxOccurs="1" name="Identity" type="tns:BIASIdentity" />
              <s:element minOccurs="0" maxOccurs="1" name="GalleryID" type="s:string" />
            </s:sequence>
          </s:extension>
        </s:complexContent>
      </s:complexType>
      <s:element name="SetBiometricDataResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="SetBiometricDataResult" type="tns:SetBiometricDataResponsePackage" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:complexType name="SetBiometricDataResponsePackage">
        <s:complexContent mixed="false">
          <s:extension base="tns:ResponseTemplate">
            <s:sequence>
              <s:element minOccurs="0" maxOccurs="1" name="Identity" type="tns:BIASIdentity" />
            </s:sequence>
          </s:extension>
        </s:complexContent>
      </s:complexType>
      <s:element name="TransformBiometricData">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="TransformBiometricDataRequest" type="tns:TransformBiometricDataRequest" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:complexType name="TransformBiometricDataRequest">
        <s:complexContent mixed="false">
          <s:extension base="tns:RequestTemplate">
            <s:sequence>
              <s:element minOccurs="0" maxOccurs="1" name="InputBIR" type="tns:CBEFF_BIR_Type" />
              <s:element minOccurs="1" maxOccurs="1" name="TransformOperation" type="s:unsignedLong" />
              <s:element minOccurs="0" maxOccurs="1" name="TransformControl" type="s:string" />
            </s:sequence>
          </s:extension>
        </s:complexContent>
      </s:complexType>
      <s:element name="TransformBiometricDataResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="TransformBiometricDataResult" type="tns:TransformBiometricDataResponsePackage" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:complexType name="TransformBiometricDataResponsePackage">
        <s:complexContent mixed="false">
          <s:extension base="tns:ResponseTemplate">
            <s:sequence>
              <s:element minOccurs="0" maxOccurs="1" name="OutputBIR" type="tns:CBEFF_BIR_Type" />
            </s:sequence>
          </s:extension>
        </s:complexContent>
      </s:complexType>
      <s:element name="UpdateBiographicData">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="UpdateBiographicDataRequest" type="tns:UpdateBiographicDataRequest" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:complexType name="UpdateBiographicDataRequest">
        <s:complexContent mixed="false">
          <s:extension base="tns:RequestTemplate">
            <s:sequence>
              <s:element minOccurs="0" maxOccurs="1" name="Identity" type="tns:BIASIdentity" />
            </s:sequence>
          </s:extension>
        </s:complexContent>
      </s:complexType>
      <s:element name="UpdateBiographicDataResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="UpdateBiographicDataResult" type="tns:UpdateBiographicDataResponsePackage" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:complexType name="UpdateBiographicDataResponsePackage">
        <s:complexContent mixed="false">
          <s:extension base="tns:ResponseTemplate" />
        </s:complexContent>
      </s:complexType>
      <s:element name="UpdateBiometricData">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="UpdateBiometricDataRequest" type="tns:UpdateBiometricDataRequest" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:complexType name="UpdateBiometricDataRequest">
        <s:complexContent mixed="false">
          <s:extension base="tns:RequestTemplate">
            <s:sequence>
              <s:element minOccurs="0" maxOccurs="1" name="Identity" type="tns:BIASIdentity" />
              <s:element minOccurs="1" maxOccurs="1" name="Merge" type="s:boolean" />
            </s:sequence>
          </s:extension>
        </s:complexContent>
      </s:complexType>
      <s:element name="UpdateBiometricDataResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="UpdateBiometricDataResult" type="tns:UpdateBiometricDataResponsePackage" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:complexType name="UpdateBiometricDataResponsePackage">
        <s:complexContent mixed="false">
          <s:extension base="tns:ResponseTemplate" />
        </s:complexContent>
      </s:complexType>
      <s:element name="Verify">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="VerifyRequest" type="tns:VerifyRequest" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:complexType name="VerifyRequest">
        <s:complexContent mixed="false">
          <s:extension base="tns:AggregateRequestTemplate">
            <s:sequence>
              <s:element minOccurs="0" maxOccurs="1" name="Identity" type="tns:BIASIdentity" />
              <s:element minOccurs="0" maxOccurs="1" name="GalleryID" type="s:string" />
            </s:sequence>
          </s:extension>
        </s:complexContent>
      </s:complexType>
      <s:element name="VerifyResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="VerifyResult" type="tns:VerifyResponsePackage" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:complexType name="VerifyResponsePackage">
        <s:complexContent mixed="false">
          <s:extension base="tns:AggregateResponseTemplate">
            <s:sequence>
              <s:element minOccurs="1" maxOccurs="1" name="Match" type="s:boolean" />
              <s:element minOccurs="0" maxOccurs="1" name="Score" type="tns:ScoreType" />
              <s:element minOccurs="0" maxOccurs="1" name="Identity" type="tns:BIASIdentity" />
              <s:element minOccurs="0" maxOccurs="1" name="Token" type="tns:TokenType" />
            </s:sequence>
          </s:extension>
        </s:complexContent>
      </s:complexType>
      <s:element name="VerifySubject">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="VerifySubjectRequest" type="tns:VerifySubjectRequest" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:complexType name="VerifySubjectRequest">
        <s:complexContent mixed="false">
          <s:extension base="tns:RequestTemplate">
            <s:sequence>
              <s:element minOccurs="0" maxOccurs="1" name="GalleryID" type="s:string" />
              <s:element minOccurs="0" maxOccurs="1" name="Identity" type="tns:BIASIdentity" />
            </s:sequence>
          </s:extension>
        </s:complexContent>
      </s:complexType>
      <s:element name="VerifySubjectResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="VerifySubjectResult" type="tns:VerifySubjectResponsePackage" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:complexType name="VerifySubjectResponsePackage">
        <s:complexContent mixed="false">
          <s:extension base="tns:ResponseTemplate">
            <s:sequence>
              <s:element minOccurs="1" maxOccurs="1" name="Match" type="s:boolean" />
              <s:element minOccurs="0" maxOccurs="1" name="Score" type="tns:ScoreType" />
            </s:sequence>
          </s:extension>
        </s:complexContent>
      </s:complexType>
      <s:complexType name="BiographicDataType" mixed="true">
        <s:sequence>
          <s:any minOccurs="0" maxOccurs="unbounded" processContents="lax" />
        </s:sequence>
        <s:anyAttribute />
      </s:complexType>
    </s:schema>
    <s:schema xmlns:tns="http://standards.iso.org/iso-iec/19785/-3/ed-2/" elementFormDefault="qualified" targetNamespace="http://standards.iso.org/iso-iec/19785/-3/ed-2/">
      <s:complexType name="MultipleTypesType" mixed="true">
        <s:sequence>
          <s:any minOccurs="0" maxOccurs="unbounded" processContents="lax" />
        </s:sequence>
        <s:anyAttribute />
      </s:complexType>
      <s:complexType name="QualityType" mixed="true">
        <s:sequence>
          <s:any minOccurs="0" maxOccurs="unbounded" processContents="lax" />
        </s:sequence>
        <s:anyAttribute />
      </s:complexType>
      <s:complexType name="SubtypeType" mixed="true">
        <s:sequence>
          <s:any minOccurs="0" maxOccurs="unbounded" processContents="lax" />
        </s:sequence>
        <s:anyAttribute />
      </s:complexType>
    </s:schema>
  </wsdl:types>
  <wsdl:message name="HelloWorldSoapIn">
    <wsdl:part name="parameters" element="tns:HelloWorld" />
  </wsdl:message>
  <wsdl:message name="HelloWorldSoapOut">
    <wsdl:part name="parameters" element="tns:HelloWorldResponse" />
  </wsdl:message>
  <wsdl:message name="AddSubjectToGallerySoapIn">
    <wsdl:part name="parameters" element="tns:AddSubjectToGallery" />
  </wsdl:message>
  <wsdl:message name="AddSubjectToGallerySoapOut">
    <wsdl:part name="parameters" element="tns:AddSubjectToGalleryResponse" />
  </wsdl:message>
  <wsdl:message name="CheckQualitySoapIn">
    <wsdl:part name="parameters" element="tns:CheckQuality" />
  </wsdl:message>
  <wsdl:message name="CheckQualitySoapOut">
    <wsdl:part name="parameters" element="tns:CheckQualityResponse" />
  </wsdl:message>
  <wsdl:message name="ClassifyBiometricDataSoapIn">
    <wsdl:part name="parameters" element="tns:ClassifyBiometricData" />
  </wsdl:message>
  <wsdl:message name="ClassifyBiometricDataSoapOut">
    <wsdl:part name="parameters" element="tns:ClassifyBiometricDataResponse" />
  </wsdl:message>
  <wsdl:message name="CreateSubjectSoapIn">
    <wsdl:part name="parameters" element="tns:CreateSubject" />
  </wsdl:message>
  <wsdl:message name="CreateSubjectSoapOut">
    <wsdl:part name="parameters" element="tns:CreateSubjectResponse" />
  </wsdl:message>
  <wsdl:message name="DeleteBiographicDataSoapIn">
    <wsdl:part name="parameters" element="tns:DeleteBiographicData" />
  </wsdl:message>
  <wsdl:message name="DeleteBiographicDataSoapOut">
    <wsdl:part name="parameters" element="tns:DeleteBiographicDataResponse" />
  </wsdl:message>
  <wsdl:message name="DeleteBiometricDataSoapIn">
    <wsdl:part name="parameters" element="tns:DeleteBiometricData" />
  </wsdl:message>
  <wsdl:message name="DeleteBiometricDataSoapOut">
    <wsdl:part name="parameters" element="tns:DeleteBiometricDataResponse" />
  </wsdl:message>
  <wsdl:message name="DeleteSubjectSoapIn">
    <wsdl:part name="parameters" element="tns:DeleteSubject" />
  </wsdl:message>
  <wsdl:message name="DeleteSubjectSoapOut">
    <wsdl:part name="parameters" element="tns:DeleteSubjectResponse" />
  </wsdl:message>
  <wsdl:message name="DeleteSubjectFromGallerySoapIn">
    <wsdl:part name="parameters" element="tns:DeleteSubjectFromGallery" />
  </wsdl:message>
  <wsdl:message name="DeleteSubjectFromGallerySoapOut">
    <wsdl:part name="parameters" element="tns:DeleteSubjectFromGalleryResponse" />
  </wsdl:message>
  <wsdl:message name="EnrollSoapIn">
    <wsdl:part name="parameters" element="tns:Enroll" />
  </wsdl:message>
  <wsdl:message name="EnrollSoapOut">
    <wsdl:part name="parameters" element="tns:EnrollResponse" />
  </wsdl:message>
  <wsdl:message name="GetEnrollResultsSoapIn">
    <wsdl:part name="parameters" element="tns:GetEnrollResults" />
  </wsdl:message>
  <wsdl:message name="GetEnrollResultsSoapOut">
    <wsdl:part name="parameters" element="tns:GetEnrollResultsResponse" />
  </wsdl:message>
  <wsdl:message name="GetIdentifyResultsSoapIn">
    <wsdl:part name="parameters" element="tns:GetIdentifyResults" />
  </wsdl:message>
  <wsdl:message name="GetIdentifyResultsSoapOut">
    <wsdl:part name="parameters" element="tns:GetIdentifyResultsResponse" />
  </wsdl:message>
  <wsdl:message name="GetIdentifySubjectResultsSoapIn">
    <wsdl:part name="parameters" element="tns:GetIdentifySubjectResults" />
  </wsdl:message>
  <wsdl:message name="GetIdentifySubjectResultsSoapOut">
    <wsdl:part name="parameters" element="tns:GetIdentifySubjectResultsResponse" />
  </wsdl:message>
  <wsdl:message name="GetVerifyResultsSoapIn">
    <wsdl:part name="parameters" element="tns:GetVerifyResults" />
  </wsdl:message>
  <wsdl:message name="GetVerifyResultsSoapOut">
    <wsdl:part name="parameters" element="tns:GetVerifyResultsResponse" />
  </wsdl:message>
  <wsdl:message name="IdentifySoapIn">
    <wsdl:part name="parameters" element="tns:Identify" />
  </wsdl:message>
  <wsdl:message name="IdentifySoapOut">
    <wsdl:part name="parameters" element="tns:IdentifyResponse" />
  </wsdl:message>
  <wsdl:message name="IdentifySubjectSoapIn">
    <wsdl:part name="parameters" element="tns:IdentifySubject" />
  </wsdl:message>
  <wsdl:message name="IdentifySubjectSoapOut">
    <wsdl:part name="parameters" element="tns:IdentifySubjectResponse" />
  </wsdl:message>
  <wsdl:message name="ListBiographicDataSoapIn">
    <wsdl:part name="parameters" element="tns:ListBiographicData" />
  </wsdl:message>
  <wsdl:message name="ListBiographicDataSoapOut">
    <wsdl:part name="parameters" element="tns:ListBiographicDataResponse" />
  </wsdl:message>
  <wsdl:message name="ListBiometricDataSoapIn">
    <wsdl:part name="parameters" element="tns:ListBiometricData" />
  </wsdl:message>
  <wsdl:message name="ListBiometricDataSoapOut">
    <wsdl:part name="parameters" element="tns:ListBiometricDataResponse" />
  </wsdl:message>
  <wsdl:message name="PerformFusionSoapIn">
    <wsdl:part name="parameters" element="tns:PerformFusion" />
  </wsdl:message>
  <wsdl:message name="PerformFusionSoapOut">
    <wsdl:part name="parameters" element="tns:PerformFusionResponse" />
  </wsdl:message>
  <wsdl:message name="QueryCapabilitiesSoapIn">
    <wsdl:part name="parameters" element="tns:QueryCapabilities" />
  </wsdl:message>
  <wsdl:message name="QueryCapabilitiesSoapOut">
    <wsdl:part name="parameters" element="tns:QueryCapabilitiesResponse" />
  </wsdl:message>
  <wsdl:message name="RetrieveBiographicDataSoapIn">
    <wsdl:part name="parameters" element="tns:RetrieveBiographicData" />
  </wsdl:message>
  <wsdl:message name="RetrieveBiographicDataSoapOut">
    <wsdl:part name="parameters" element="tns:RetrieveBiographicDataResponse" />
  </wsdl:message>
  <wsdl:message name="RetrieveBiometricDataSoapIn">
    <wsdl:part name="parameters" element="tns:RetrieveBiometricData" />
  </wsdl:message>
  <wsdl:message name="RetrieveBiometricDataSoapOut">
    <wsdl:part name="parameters" element="tns:RetrieveBiometricDataResponse" />
  </wsdl:message>
  <wsdl:message name="RetrieveDataSoapIn">
    <wsdl:part name="parameters" element="tns:RetrieveData" />
  </wsdl:message>
  <wsdl:message name="RetrieveDataSoapOut">
    <wsdl:part name="parameters" element="tns:RetrieveDataResponse" />
  </wsdl:message>
  <wsdl:message name="SetBiographicDataSoapIn">
    <wsdl:part name="parameters" element="tns:SetBiographicData" />
  </wsdl:message>
  <wsdl:message name="SetBiographicDataSoapOut">
    <wsdl:part name="parameters" element="tns:SetBiographicDataResponse" />
  </wsdl:message>
  <wsdl:message name="SetBiometricDataSoapIn">
    <wsdl:part name="parameters" element="tns:SetBiometricData" />
  </wsdl:message>
  <wsdl:message name="SetBiometricDataSoapOut">
    <wsdl:part name="parameters" element="tns:SetBiometricDataResponse" />
  </wsdl:message>
  <wsdl:message name="TransformBiometricDataSoapIn">
    <wsdl:part name="parameters" element="tns:TransformBiometricData" />
  </wsdl:message>
  <wsdl:message name="TransformBiometricDataSoapOut">
    <wsdl:part name="parameters" element="tns:TransformBiometricDataResponse" />
  </wsdl:message>
  <wsdl:message name="UpdateBiographicDataSoapIn">
    <wsdl:part name="parameters" element="tns:UpdateBiographicData" />
  </wsdl:message>
  <wsdl:message name="UpdateBiographicDataSoapOut">
    <wsdl:part name="parameters" element="tns:UpdateBiographicDataResponse" />
  </wsdl:message>
  <wsdl:message name="UpdateBiometricDataSoapIn">
    <wsdl:part name="parameters" element="tns:UpdateBiometricData" />
  </wsdl:message>
  <wsdl:message name="UpdateBiometricDataSoapOut">
    <wsdl:part name="parameters" element="tns:UpdateBiometricDataResponse" />
  </wsdl:message>
  <wsdl:message name="VerifySoapIn">
    <wsdl:part name="parameters" element="tns:Verify" />
  </wsdl:message>
  <wsdl:message name="VerifySoapOut">
    <wsdl:part name="parameters" element="tns:VerifyResponse" />
  </wsdl:message>
  <wsdl:message name="VerifySubjectSoapIn">
    <wsdl:part name="parameters" element="tns:VerifySubject" />
  </wsdl:message>
  <wsdl:message name="VerifySubjectSoapOut">
    <wsdl:part name="parameters" element="tns:VerifySubjectResponse" />
  </wsdl:message>
  <wsdl:portType name="BIASSoap">
    <wsdl:operation name="HelloWorld">
      <wsdl:input message="tns:HelloWorldSoapIn" />
      <wsdl:output message="tns:HelloWorldSoapOut" />
    </wsdl:operation>
    <wsdl:operation name="AddSubjectToGallery">
      <wsdl:input message="tns:AddSubjectToGallerySoapIn" />
      <wsdl:output message="tns:AddSubjectToGallerySoapOut" />
    </wsdl:operation>
    <wsdl:operation name="CheckQuality">
      <wsdl:input message="tns:CheckQualitySoapIn" />
      <wsdl:output message="tns:CheckQualitySoapOut" />
    </wsdl:operation>
    <wsdl:operation name="ClassifyBiometricData">
      <wsdl:input message="tns:ClassifyBiometricDataSoapIn" />
      <wsdl:output message="tns:ClassifyBiometricDataSoapOut" />
    </wsdl:operation>
    <wsdl:operation name="CreateSubject">
      <wsdl:input message="tns:CreateSubjectSoapIn" />
      <wsdl:output message="tns:CreateSubjectSoapOut" />
    </wsdl:operation>
    <wsdl:operation name="DeleteBiographicData">
      <wsdl:input message="tns:DeleteBiographicDataSoapIn" />
      <wsdl:output message="tns:DeleteBiographicDataSoapOut" />
    </wsdl:operation>
    <wsdl:operation name="DeleteBiometricData">
      <wsdl:input message="tns:DeleteBiometricDataSoapIn" />
      <wsdl:output message="tns:DeleteBiometricDataSoapOut" />
    </wsdl:operation>
    <wsdl:operation name="DeleteSubject">
      <wsdl:input message="tns:DeleteSubjectSoapIn" />
      <wsdl:output message="tns:DeleteSubjectSoapOut" />
    </wsdl:operation>
    <wsdl:operation name="DeleteSubjectFromGallery">
      <wsdl:input message="tns:DeleteSubjectFromGallerySoapIn" />
      <wsdl:output message="tns:DeleteSubjectFromGallerySoapOut" />
    </wsdl:operation>
    <wsdl:operation name="Enroll">
      <wsdl:input message="tns:EnrollSoapIn" />
      <wsdl:output message="tns:EnrollSoapOut" />
    </wsdl:operation>
    <wsdl:operation name="GetEnrollResults">
      <wsdl:input message="tns:GetEnrollResultsSoapIn" />
      <wsdl:output message="tns:GetEnrollResultsSoapOut" />
    </wsdl:operation>
    <wsdl:operation name="GetIdentifyResults">
      <wsdl:input message="tns:GetIdentifyResultsSoapIn" />
      <wsdl:output message="tns:GetIdentifyResultsSoapOut" />
    </wsdl:operation>
    <wsdl:operation name="GetIdentifySubjectResults">
      <wsdl:input message="tns:GetIdentifySubjectResultsSoapIn" />
      <wsdl:output message="tns:GetIdentifySubjectResultsSoapOut" />
    </wsdl:operation>
    <wsdl:operation name="GetVerifyResults">
      <wsdl:input message="tns:GetVerifyResultsSoapIn" />
      <wsdl:output message="tns:GetVerifyResultsSoapOut" />
    </wsdl:operation>
    <wsdl:operation name="Identify">
      <wsdl:input message="tns:IdentifySoapIn" />
      <wsdl:output message="tns:IdentifySoapOut" />
    </wsdl:operation>
    <wsdl:operation name="IdentifySubject">
      <wsdl:input message="tns:IdentifySubjectSoapIn" />
      <wsdl:output message="tns:IdentifySubjectSoapOut" />
    </wsdl:operation>
    <wsdl:operation name="ListBiographicData">
      <wsdl:input message="tns:ListBiographicDataSoapIn" />
      <wsdl:output message="tns:ListBiographicDataSoapOut" />
    </wsdl:operation>
    <wsdl:operation name="ListBiometricData">
      <wsdl:input message="tns:ListBiometricDataSoapIn" />
      <wsdl:output message="tns:ListBiometricDataSoapOut" />
    </wsdl:operation>
    <wsdl:operation name="PerformFusion">
      <wsdl:input message="tns:PerformFusionSoapIn" />
      <wsdl:output message="tns:PerformFusionSoapOut" />
    </wsdl:operation>
    <wsdl:operation name="QueryCapabilities">
      <wsdl:input message="tns:QueryCapabilitiesSoapIn" />
      <wsdl:output message="tns:QueryCapabilitiesSoapOut" />
    </wsdl:operation>
    <wsdl:operation name="RetrieveBiographicData">
      <wsdl:input message="tns:RetrieveBiographicDataSoapIn" />
      <wsdl:output message="tns:RetrieveBiographicDataSoapOut" />
    </wsdl:operation>
    <wsdl:operation name="RetrieveBiometricData">
      <wsdl:input message="tns:RetrieveBiometricDataSoapIn" />
      <wsdl:output message="tns:RetrieveBiometricDataSoapOut" />
    </wsdl:operation>
    <wsdl:operation name="RetrieveData">
      <wsdl:input message="tns:RetrieveDataSoapIn" />
      <wsdl:output message="tns:RetrieveDataSoapOut" />
    </wsdl:operation>
    <wsdl:operation name="SetBiographicData">
      <wsdl:input message="tns:SetBiographicDataSoapIn" />
      <wsdl:output message="tns:SetBiographicDataSoapOut" />
    </wsdl:operation>
    <wsdl:operation name="SetBiometricData">
      <wsdl:input message="tns:SetBiometricDataSoapIn" />
      <wsdl:output message="tns:SetBiometricDataSoapOut" />
    </wsdl:operation>
    <wsdl:operation name="TransformBiometricData">
      <wsdl:input message="tns:TransformBiometricDataSoapIn" />
      <wsdl:output message="tns:TransformBiometricDataSoapOut" />
    </wsdl:operation>
    <wsdl:operation name="UpdateBiographicData">
      <wsdl:input message="tns:UpdateBiographicDataSoapIn" />
      <wsdl:output message="tns:UpdateBiographicDataSoapOut" />
    </wsdl:operation>
    <wsdl:operation name="UpdateBiometricData">
      <wsdl:input message="tns:UpdateBiometricDataSoapIn" />
      <wsdl:output message="tns:UpdateBiometricDataSoapOut" />
    </wsdl:operation>
    <wsdl:operation name="Verify">
      <wsdl:input message="tns:VerifySoapIn" />
      <wsdl:output message="tns:VerifySoapOut" />
    </wsdl:operation>
    <wsdl:operation name="VerifySubject">
      <wsdl:input message="tns:VerifySubjectSoapIn" />
      <wsdl:output message="tns:VerifySubjectSoapOut" />
    </wsdl:operation>
  </wsdl:portType>
  <wsdl:binding name="BIASSoap" type="tns:BIASSoap">
    <soap:binding transport="http://schemas.xmlsoap.org/soap/http" />
    <wsdl:operation name="HelloWorld">
      <soap:operation soapAction="http://docs.oasis-open.org/bias/ns/bias-2.0/HelloWorld" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="AddSubjectToGallery">
      <soap:operation soapAction="http://docs.oasis-open.org/bias/ns/bias-2.0/AddSubjectToGallery" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="CheckQuality">
      <soap:operation soapAction="http://docs.oasis-open.org/bias/ns/bias-2.0/CheckQuality" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="ClassifyBiometricData">
      <soap:operation soapAction="http://docs.oasis-open.org/bias/ns/bias-2.0/ClassifyBiometricData" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="CreateSubject">
      <soap:operation soapAction="http://docs.oasis-open.org/bias/ns/bias-2.0/CreateSubject" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="DeleteBiographicData">
      <soap:operation soapAction="http://docs.oasis-open.org/bias/ns/bias-2.0/DeleteBiographicData" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="DeleteBiometricData">
      <soap:operation soapAction="http://docs.oasis-open.org/bias/ns/bias-2.0/DeleteBiometricData" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="DeleteSubject">
      <soap:operation soapAction="http://docs.oasis-open.org/bias/ns/bias-2.0/DeleteSubject" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="DeleteSubjectFromGallery">
      <soap:operation soapAction="http://docs.oasis-open.org/bias/ns/bias-2.0/DeleteSubjectFromGallery" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="Enroll">
      <soap:operation soapAction="http://docs.oasis-open.org/bias/ns/bias-2.0/Enroll" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="GetEnrollResults">
      <soap:operation soapAction="http://docs.oasis-open.org/bias/ns/bias-2.0/GetEnrollResults" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="GetIdentifyResults">
      <soap:operation soapAction="http://docs.oasis-open.org/bias/ns/bias-2.0/GetIdentifyResults" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="GetIdentifySubjectResults">
      <soap:operation soapAction="http://docs.oasis-open.org/bias/ns/bias-2.0/GetIdentifySubjectResults" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="GetVerifyResults">
      <soap:operation soapAction="http://docs.oasis-open.org/bias/ns/bias-2.0/GetVerifyResults" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="Identify">
      <soap:operation soapAction="http://docs.oasis-open.org/bias/ns/bias-2.0/Identify" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="IdentifySubject">
      <soap:operation soapAction="http://docs.oasis-open.org/bias/ns/bias-2.0/IdentifySubject" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="ListBiographicData">
      <soap:operation soapAction="http://docs.oasis-open.org/bias/ns/bias-2.0/ListBiographicData" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="ListBiometricData">
      <soap:operation soapAction="http://docs.oasis-open.org/bias/ns/bias-2.0/ListBiometricData" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="PerformFusion">
      <soap:operation soapAction="http://docs.oasis-open.org/bias/ns/bias-2.0/PerformFusion" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="QueryCapabilities">
      <soap:operation soapAction="http://docs.oasis-open.org/bias/ns/bias-2.0/QueryCapabilities" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="RetrieveBiographicData">
      <soap:operation soapAction="http://docs.oasis-open.org/bias/ns/bias-2.0/RetrieveBiographicData" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="RetrieveBiometricData">
      <soap:operation soapAction="http://docs.oasis-open.org/bias/ns/bias-2.0/RetrieveBiometricData" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="RetrieveData">
      <soap:operation soapAction="http://docs.oasis-open.org/bias/ns/bias-2.0/RetrieveData" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="SetBiographicData">
      <soap:operation soapAction="http://docs.oasis-open.org/bias/ns/bias-2.0/SetBiographicData" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="SetBiometricData">
      <soap:operation soapAction="http://docs.oasis-open.org/bias/ns/bias-2.0/SetBiometricData" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="TransformBiometricData">
      <soap:operation soapAction="http://docs.oasis-open.org/bias/ns/bias-2.0/TransformBiometricData" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="UpdateBiographicData">
      <soap:operation soapAction="http://docs.oasis-open.org/bias/ns/bias-2.0/UpdateBiographicData" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="UpdateBiometricData">
      <soap:operation soapAction="http://docs.oasis-open.org/bias/ns/bias-2.0/UpdateBiometricData" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="Verify">
      <soap:operation soapAction="http://docs.oasis-open.org/bias/ns/bias-2.0/Verify" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="VerifySubject">
      <soap:operation soapAction="http://docs.oasis-open.org/bias/ns/bias-2.0/VerifySubject" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
  </wsdl:binding>
  <wsdl:binding name="BIASSoap12" type="tns:BIASSoap">
    <soap12:binding transport="http://schemas.xmlsoap.org/soap/http" />
    <wsdl:operation name="HelloWorld">
      <soap12:operation soapAction="http://docs.oasis-open.org/bias/ns/bias-2.0/HelloWorld" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="AddSubjectToGallery">
      <soap12:operation soapAction="http://docs.oasis-open.org/bias/ns/bias-2.0/AddSubjectToGallery" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="CheckQuality">
      <soap12:operation soapAction="http://docs.oasis-open.org/bias/ns/bias-2.0/CheckQuality" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="ClassifyBiometricData">
      <soap12:operation soapAction="http://docs.oasis-open.org/bias/ns/bias-2.0/ClassifyBiometricData" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="CreateSubject">
      <soap12:operation soapAction="http://docs.oasis-open.org/bias/ns/bias-2.0/CreateSubject" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="DeleteBiographicData">
      <soap12:operation soapAction="http://docs.oasis-open.org/bias/ns/bias-2.0/DeleteBiographicData" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="DeleteBiometricData">
      <soap12:operation soapAction="http://docs.oasis-open.org/bias/ns/bias-2.0/DeleteBiometricData" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="DeleteSubject">
      <soap12:operation soapAction="http://docs.oasis-open.org/bias/ns/bias-2.0/DeleteSubject" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="DeleteSubjectFromGallery">
      <soap12:operation soapAction="http://docs.oasis-open.org/bias/ns/bias-2.0/DeleteSubjectFromGallery" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="Enroll">
      <soap12:operation soapAction="http://docs.oasis-open.org/bias/ns/bias-2.0/Enroll" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="GetEnrollResults">
      <soap12:operation soapAction="http://docs.oasis-open.org/bias/ns/bias-2.0/GetEnrollResults" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="GetIdentifyResults">
      <soap12:operation soapAction="http://docs.oasis-open.org/bias/ns/bias-2.0/GetIdentifyResults" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="GetIdentifySubjectResults">
      <soap12:operation soapAction="http://docs.oasis-open.org/bias/ns/bias-2.0/GetIdentifySubjectResults" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="GetVerifyResults">
      <soap12:operation soapAction="http://docs.oasis-open.org/bias/ns/bias-2.0/GetVerifyResults" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="Identify">
      <soap12:operation soapAction="http://docs.oasis-open.org/bias/ns/bias-2.0/Identify" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="IdentifySubject">
      <soap12:operation soapAction="http://docs.oasis-open.org/bias/ns/bias-2.0/IdentifySubject" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="ListBiographicData">
      <soap12:operation soapAction="http://docs.oasis-open.org/bias/ns/bias-2.0/ListBiographicData" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="ListBiometricData">
      <soap12:operation soapAction="http://docs.oasis-open.org/bias/ns/bias-2.0/ListBiometricData" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="PerformFusion">
      <soap12:operation soapAction="http://docs.oasis-open.org/bias/ns/bias-2.0/PerformFusion" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="QueryCapabilities">
      <soap12:operation soapAction="http://docs.oasis-open.org/bias/ns/bias-2.0/QueryCapabilities" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="RetrieveBiographicData">
      <soap12:operation soapAction="http://docs.oasis-open.org/bias/ns/bias-2.0/RetrieveBiographicData" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="RetrieveBiometricData">
      <soap12:operation soapAction="http://docs.oasis-open.org/bias/ns/bias-2.0/RetrieveBiometricData" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="RetrieveData">
      <soap12:operation soapAction="http://docs.oasis-open.org/bias/ns/bias-2.0/RetrieveData" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="SetBiographicData">
      <soap12:operation soapAction="http://docs.oasis-open.org/bias/ns/bias-2.0/SetBiographicData" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="SetBiometricData">
      <soap12:operation soapAction="http://docs.oasis-open.org/bias/ns/bias-2.0/SetBiometricData" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="TransformBiometricData">
      <soap12:operation soapAction="http://docs.oasis-open.org/bias/ns/bias-2.0/TransformBiometricData" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="UpdateBiographicData">
      <soap12:operation soapAction="http://docs.oasis-open.org/bias/ns/bias-2.0/UpdateBiographicData" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="UpdateBiometricData">
      <soap12:operation soapAction="http://docs.oasis-open.org/bias/ns/bias-2.0/UpdateBiometricData" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="Verify">
      <soap12:operation soapAction="http://docs.oasis-open.org/bias/ns/bias-2.0/Verify" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="VerifySubject">
      <soap12:operation soapAction="http://docs.oasis-open.org/bias/ns/bias-2.0/VerifySubject" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
  </wsdl:binding>
  <wsdl:service name="BIAS">
    <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">BIAS Tester Service</wsdl:documentation>
    <wsdl:port name="BIASSoap" binding="tns:BIASSoap">
      <soap:address location="http://localhost:58959/BIAS.asmx" />
    </wsdl:port>
    <wsdl:port name="BIASSoap12" binding="tns:BIASSoap12">
      <soap12:address location="http://localhost:58959/BIAS.asmx" />
    </wsdl:port>
  </wsdl:service>
</wsdl:definitions>