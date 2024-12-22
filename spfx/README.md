# sgart-spfx-delegate

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
