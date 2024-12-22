# sgart-spfx-api-delegate
Demo chiamata a custom API da una web part SPFx con delegate permission

## APP REGISTRATION

Creare una app registration in Entra Id https://portal.azure.com/#view/Microsoft_AAD_IAM/ActiveDirectoryMenuBlade/~/RegisteredApps

- Segnarsi:
    - Nome: **SgartSPFxDelegateDemoApp**
    - TenantId: xxxxxxxx-xxxx-xxxx-xxxxxxxxxxxxxxxxx
    - ClientId: xxxxxxxx-xxxx-xxxx-xxxxxxxxxxxxxxxxx
    - TenantName: tenantName.sharepoint.com
- API Permissions:
    -  Delegate:
		- User.Read
- Expose an API:
    - AplicationID Uri: **api://xxxxxxxx-xxxx-xxxx-xxxxxxxxxxxxxxxxx**
    - Scopes:
        - Name: **user_impersonation**
        - Who can consent? Admins Only
        - Admin consent display name: Access SgartSPFxDelegateDemoApp
        - Admin consent description: Allow the application to access SgartSPFxDelegateDemoApp on behalf of the signed-in user.
        - State: Enabled

## SPFx

SPFX v1.20.* https://learn.microsoft.com/en-us/sharepoint/dev/spfx/compatibility 

### Creazione progetto SPFxSPFx

nvm use 18.18.2

npm install gulp-cli yo @microsoft/generator-sharepoint --global

gulp trust-dev-cert

cd spfx

yo @microsoft/sharepoint

### Configurazione APP Registration

package-solution.json

"webApiPermissionRequests": [
    {
        "resource": "SgartSPFxDelegateDemoApp",
        "scope": "user_impersonation"
    }
]

dove:
- resource: **displayName** dell'app registration
- scope: nome dello scope definito nella app registration sotto **Expose an API**

### ATTIVAZIONE PERMISSIONS

Perchè funzionino le chiamate alle API Custom, è necessario fare un deploy nell'app catalog https://sgart.sharepoint.com/sites/AppCatalog/_layouts/15/tenantAppCatalog.aspx/manageApps 

In seguito devono essere approvate le permission configurate **package-solution.json** sezione **webApiPermissionRequests** https://tenantName-admin.sharepoint.com/_layouts/15/online/AdminHome.aspx#/webApiPermissionManagement

## Approve access so this app works as designed
For this app to work as designed, it needs additional API access. To approve the permission request now, select Go to API access page, and then look under Pending requests.

To approve access to third-party APIs, you must be signed in as an Application Administrator. To approve access to Microsoft APIs, you must be signed in as a Global Administrator.


### DEBUG
code .

settare la variabile di ambiente SPFX_SERVE_TENANT_DOMAIN=tenantName.sharepoint.com

gulp serve

### DEPLOY

gulp clear; gulp bundle --ship; gulp package-solution --ship


### Note

Esempio di chiamata effettuata da SPFx per ottenere il token

GET
	https://sgart.sharepoint.com/_api/Microsoft.SharePoint.Internal.ClientSideComponent.Token.AcquireOBOToken?resource='SgartSPFxDelegateDemoApp'&clientId='b997be1f-4b9a-447f-ad36-490f25651f43'

{
	"odata.error": {
		"code": "-1, System.AggregateException",
		"message": {
			"lang": "en-US",
			"value": "One or more errors occurred."
		}
	}
}


## Custom API .NET 8

### appsettings.json

  "AzureAd": {
    "Instance": "https://login.microsoftonline.com/",
    "Domain": "tenantName.sharepoint.com",
    "TenantId": "xxxxxxxx-xxxx-xxxx-xxxxxxxxxxxxxxxxx",
    "ClientId": "xxxxxxxx-xxxx-xxxx-xxxxxxxxxxxxxxxxx",
    "Scopes": "user_impersonation",
    "CallbackPath": "/signin-oidc"
  }


### Link utili

OAuth 2.0 implicit flow https://learn.microsoft.com/en-us/entra/identity-platform/v2-oauth2-implicit-grant-flow

Come configurare API con Entra ID https://learn.microsoft.com/en-us/sharepoint/dev/spfx/use-aadhttpclient

https://learn.microsoft.com/en-us/sharepoint/dev/spfx/use-aadhttpclient-enterpriseapi

