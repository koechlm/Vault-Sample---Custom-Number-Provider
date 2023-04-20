Overview:
CustomNumbering demonstrates the use of user-created numbering generation on the Vault Server, and 
its interactions with the Vault Client.

Find a powerpoint slide introducing and explaining this sample more in detail in the subfolder of this solution: https://github.com/koechlm/Vault-Sample---Custom-Number-Provider/blob/master/CustomNumberingProvider/Autodesk%20Vault%20Professional%20-%20Custom%20Numbering%20Sample.pptx. All supplemental files listed in the slide deck can be found within the same solution subfolder.

CustomNumbering contains the following features/concepts:
- Select entity class with combobox control
- Manage number schemes that utilize CustomNumberProvider via button controls
- View generated numbers or error codes from generation restrictions / failures for a selected scheme

To use:
Open CustomNumbering.sln in Visual Studio 2017. The projects should open and build
with no errors. You must copy CustomNumberingProvider.dll from the SDK's
CustomNumberingProvider bin directory to your Vault Server's .../web/services/bin/ 
directory, and include the following lines in your Vault Server's
.../web/services/web.config, under the connectivity.core/numberProviders node:

<numberProvider name="CustomNumberingProvider" type="CustomNumberingProvider.CustomNumberingProvider, CustomNumberingProvider" canCache="false">
    <!-- <initializationParm value="Your Value Here" /> -->
</numberProvider>

To complete inclusion of CustomNumberingProvider on the Vault Server, perform an IIS reset.

Notes:
- INumberProvider has been moved from Connectivity.Platform to Autodesk.DataManagement.Server.Extensibility
    - Number Scheme Fields have also been moved to Autodesk.DataManagement.Server.Extensibility
- GenerateNumber() was removed from INumberProvider
    - GenerateNumbers() should be used in its place
