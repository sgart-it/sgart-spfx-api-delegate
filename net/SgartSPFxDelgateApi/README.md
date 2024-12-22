# SgartSPFxDelgateApi

- Creare una app registration in Entra Id https://portal.azure.com/#view/Microsoft_AAD_IAM/ActiveDirectoryMenuBlade/~/RegisteredApps
- Segnarsi:
    - Nome: SgartSPFxDelegateDemoApp
    - TenantId: xxxxxxxx-xxxx-xxxx-xxxxxxxxxxxxxxxxx
    - ClientId: xxxxxxxx-xxxx-xxxx-xxxxxxxxxxxxxxxxx
    - TenantName: tenantName.sharepoint.com
- API Permissions:
    -  Delegate:
     - User.Read
- Expose an API:
    - AplicationID Uri: api://xxxxxxxx-xxxx-xxxx-xxxxxxxxxxxxxxxxx
    - Scopes:
        - Name: user_impersonation
        - Who can consent? Admins Only
        - Admin consent display name: Access SgartSPFxDelegateDemoApp
        - Admin consent description: Allow the application to access SgartSPFxDelegateDemoApp on behalf of the signed-in user.
        - State: Enabled


