//import * as React from 'react';
import * as ReactDom from 'react-dom';
import { Version } from '@microsoft/sp-core-library';
import {
  type IPropertyPaneConfiguration,
  PropertyPaneTextField
} from '@microsoft/sp-property-pane';
import { BaseClientSideWebPart } from '@microsoft/sp-webpart-base';
import { IReadonlyTheme } from '@microsoft/sp-component-base';

import * as strings from 'SgartDemoDelegateWebPartStrings';
//import SgartDemoDelegate from './components/SgartDemoDelegate';
//import { ISgartDemoDelegateProps } from './components/ISgartDemoDelegateProps';

import { AadHttpClient, HttpClientResponse, IHttpClientOptions } from '@microsoft/sp-http';

export interface ISgartDemoDelegateWebPartProps {
  description: string;
}
const API_BASE_URL = "https://localhost:7101";
const APPLICATION_ID_URI = "api://78ed870c-b931-442e-91ff-db65722fb9d4";

export default class SgartDemoDelegateWebPart extends BaseClientSideWebPart<ISgartDemoDelegateWebPartProps> {

  //private _isDarkTheme: boolean = false;
  //private _environmentMessage: string = '';
  private apiClient: AadHttpClient;

  protected onInit(): Promise<void> {
    // inizializzo il client
    console.log('Sgart: api url:', API_BASE_URL);
    return new Promise<void>((resolve: () => void, reject: (error: any) => void): void => {
      this.context.aadHttpClientFactory
        .getClient(APPLICATION_ID_URI)
        .then((client: AadHttpClient): void => {
          this.apiClient = client;
          //console.log('Sgart: token message:', (client as any)._aadTokenProvider._tokenAcquisitionEvent._name);
          console.log('Sgart: client:', this.apiClient);

          resolve();
        }, (err: any) => {
          console.error("Sgart: getClient", err);
          return reject(err)
        });

    });
    /*return this._getEnvironmentMessage().then(message => {
      this._environmentMessage = message;
    });*/
  }

  /* GET

      this.apiClient
      .get(API_BASE_URL + '/demo',  AadHttpClient.configurations.v1)
      .then((res: HttpClientResponse): Promise<any> => {
        return res.json();
      })
      .then((data: any): void => {
        console.log("Sgart: data", data);
        this.context.statusRenderer.clearLoadingIndicator(this.domElement);
        this.domElement.innerHTML = `
          <pre>${JSON.stringify(data, null, 4)}</pre>`;
      }, (err: any): void => {
        console.error("Sgart: err", err);
        this.context.statusRenderer.renderError(this.domElement, err);
      });
  */

  public render(): void {
    this.context.statusRenderer.displayLoadingIndicator(this.domElement, 'api');

    /*
    this.apiClient
    .get(API_BASE_URL + '/demo',  AadHttpClient.configurations.v1)
    .then((res: HttpClientResponse): Promise<any> => {
      return res.json();
    })
    .then((data: any): void => {
      console.log("Sgart: data", data);
      this.context.statusRenderer.clearLoadingIndicator(this.domElement);
      this.domElement.innerHTML = `
        <pre>${JSON.stringify(data, null, 4)}</pre>`;
    }, (err: any): void => {
      console.error("Sgart: err", err);
      this.context.statusRenderer.renderError(this.domElement, err);
    });
    */


    // faccio una chiamata in post
    const body = {
      param1: 1
    };

    const requestHeaders: Headers = new Headers();
    requestHeaders.append('Content-type', 'application/json');
    //requestHeaders.append('authorization', `Bearer ${accessToken}`);

    const options: IHttpClientOptions = {
      headers: requestHeaders,
      body: JSON.stringify(body)
    };

    this.apiClient
      .post(API_BASE_URL + '/demo', AadHttpClient.configurations.v1, options)
      .then((res: HttpClientResponse): Promise<any> => {
        console.log("Sgart: res");
        return res.json();
        //return res.text();
      })
      .then((data: any): void => {
        console.log("Sgart: data", data);
        this.context.statusRenderer.clearLoadingIndicator(this.domElement);
        this.domElement.innerHTML = `
            <pre>${JSON.stringify(data, null, 4)}</pre>`;
      }, (err: any): void => {
        console.error("Sgart: err", err);
        this.context.statusRenderer.renderError(this.domElement, err);
      });


    /*
     this.context.aadTokenProviderFactory.getTokenProvider()
      .then( o => o.getToken(APPLICATION_ID_URI))
      //.then( p => console.log("Sgart token:", p));
      .then(accessToken => {
        console.log('Sgart: accessToken', accessToken);
      });
    */

    /*const element: React.ReactElement<ISgartDemoDelegateProps> = React.createElement(
      SgartDemoDelegate,
      {
        description: this.properties.description,
        isDarkTheme: this._isDarkTheme,
        environmentMessage: this._environmentMessage,
        hasTeamsContext: !!this.context.sdks.microsoftTeams,
        userDisplayName: this.context.pageContext.user.displayName,
        tokenOk: false
      }
    );

    ReactDom.render(element, this.domElement);*/
  }


  /*
    private _getEnvironmentMessage(): Promise<string> {
      if (!!this.context.sdks.microsoftTeams) { // running in Teams, office.com or Outlook
        return this.context.sdks.microsoftTeams.teamsJs.app.getContext()
          .then(context => {
            let environmentMessage: string = '';
            switch (context.app.host.name) {
              case 'Office': // running in Office
                environmentMessage = this.context.isServedFromLocalhost ? strings.AppLocalEnvironmentOffice : strings.AppOfficeEnvironment;
                break;
              case 'Outlook': // running in Outlook
                environmentMessage = this.context.isServedFromLocalhost ? strings.AppLocalEnvironmentOutlook : strings.AppOutlookEnvironment;
                break;
              case 'Teams': // running in Teams
              case 'TeamsModern':
                environmentMessage = this.context.isServedFromLocalhost ? strings.AppLocalEnvironmentTeams : strings.AppTeamsTabEnvironment;
                break;
              default:
                environmentMessage = strings.UnknownEnvironment;
            }
  
            return environmentMessage;
          });
      }
  
      return Promise.resolve(this.context.isServedFromLocalhost ? strings.AppLocalEnvironmentSharePoint : strings.AppSharePointEnvironment);
    }
  */
  protected onThemeChanged(currentTheme: IReadonlyTheme | undefined): void {
    if (!currentTheme) {
      return;
    }

    //this._isDarkTheme = !!currentTheme.isInverted;
    const {
      semanticColors
    } = currentTheme;

    if (semanticColors) {
      this.domElement.style.setProperty('--bodyText', semanticColors.bodyText || null);
      this.domElement.style.setProperty('--link', semanticColors.link || null);
      this.domElement.style.setProperty('--linkHovered', semanticColors.linkHovered || null);
    }

  }

  protected onDispose(): void {
    ReactDom.unmountComponentAtNode(this.domElement);
  }

  protected get dataVersion(): Version {
    return Version.parse('1.0');
  }

  protected getPropertyPaneConfiguration(): IPropertyPaneConfiguration {
    return {
      pages: [
        {
          header: {
            description: strings.PropertyPaneDescription
          },
          groups: [
            {
              groupName: strings.BasicGroupName,
              groupFields: [
                PropertyPaneTextField('description', {
                  label: strings.DescriptionFieldLabel
                })
              ]
            }
          ]
        }
      ]
    };
  }
}
