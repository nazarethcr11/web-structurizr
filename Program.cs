using Structurizr;
using Structurizr.Api;

namespace c4_model_design{

    class Program
    {
        static void Main(string[] args)
        {
            Banking();
        }

        static void Banking()
        {
            const long workspaceId = 73392;
            const string apiKey = "3cbcf952-5807-4513-9376-29c984835a85";
            const string apiSecret = "55eceb72-d6b6-4ba5-8f39-55e88ec5cd44";

            StructurizrClient structurizrClient = new StructurizrClient(apiKey, apiSecret);
            //nombre
            Workspace workspace = new Workspace("Telaxia App", "Sistema intermediario entre diseñadores-vendedores");
            ViewSet viewSet = workspace.Views;
            Model model = workspace.Model;

            // 1. Diagrama de Contexto

            //sistemas
            SoftwareSystem payU = model.AddSoftwareSystem("PayU", "Plataforma que ofrece una REST API de pago.");
            SoftwareSystem gmail = model.AddSoftwareSystem("GoogleEmail", "Plataforma que ofrece una REST API de correo.");
           
            
            SoftwareSystem telaxiaApp = model.AddSoftwareSystem("Telaxia App", "Aplicacion de venta de  diseños de ropa");

            
            Person adminUser = model.AddPerson("Administator", "Usuario que administra la app");
            Person designUser = model.AddPerson("Designer", "Usuario que publica y diseña prendas de vestir");
            Person buyUser = model.AddPerson("Buyer", "Usuario que realiza la compra de diseños ");
           

            
            adminUser.Uses(telaxiaApp, "Administra la aplicacion");
            designUser.Uses(telaxiaApp, "Publica y diseña prendas de vestir");
            buyUser.Uses(telaxiaApp, "Realiza la compra de diseños");
           
    
            telaxiaApp.Uses(payU, "Usa la API de PayU ");
            telaxiaApp.Uses(gmail, "Usa la API de Gmail ");
           


            SystemContextView contextView = viewSet.CreateSystemContextView(telaxiaApp, "Contexto", "Diagrama de contexto");
            contextView.PaperSize = PaperSize.A4_Landscape;
            contextView.AddAllSoftwareSystems();
            contextView.AddAllPeople();

            // Tags

            adminUser.AddTags("Usuario que administra");
            designUser.AddTags("Publica y diseña prendas de vestir");
            buyUser.AddTags("Realiza la compra de diseños");
            telaxiaApp.AddTags("Telaxia App");
            payU.AddTags("PayU");
            gmail.AddTags("GoogleEmail");
           
          

            Styles styles = viewSet.Configuration.Styles;
            styles.Add(new ElementStyle("Usuario que administra")
                {Background = "#0a60ff", Color = "#ffffff", Shape = Shape.Person});
            styles.Add(new ElementStyle("Publica y diseña prendas de vestir")
                {Background = "#0a60ff", Color = "#ffffff", Shape = Shape.Person});
            styles.Add(new ElementStyle("Realiza la compra de diseños")
                {Background = "#0a60ff", Color = "#ffffff", Shape = Shape.Person});
            styles.Add(new ElementStyle("Telaxia App")
                {Background = "#008f39", Color = "#ffffff", Shape = Shape.RoundedBox});
            styles.Add(new ElementStyle("PayU")
                {Background = "#2f95c7", Color = "#ffffff", Shape = Shape.RoundedBox});
            styles.Add(new ElementStyle("GoogleEmail")
                {Background = "#2f95c7", Color = "#ffffff", Shape = Shape.RoundedBox});


            // 2. Diagrama de Contenedores

            Container mobileApplication = telaxiaApp.AddContainer("Mobile App", "", "Vue");
            Container webApplication = telaxiaApp.AddContainer("Web App", "", "Vue");
            Container landingPage = telaxiaApp.AddContainer("Landing Page", "", "Vue");
            Container apiRest = telaxiaApp.AddContainer("API Rest", "API Rest", "Javascript, C# port 8080");
            Container dataBase = telaxiaApp.AddContainer("DataBase", "", "SQL Server");
            
            //others
            mobileApplication.Uses(apiRest, "API Request", "JSON/HTTPS");
            webApplication.Uses(apiRest, "API Request", "JSON/HTTPS");
            landingPage.Uses(apiRest, "API Request", "JSON/HTTPS");
            
            //usuarios uses
            //mobile app
            adminUser.Uses(mobileApplication, "", "");
            designUser.Uses(mobileApplication, "", "");
            buyUser.Uses(mobileApplication, "", "");
            
            //web app
            adminUser.Uses(webApplication, "", "");
            designUser.Uses(webApplication, "", "");
            buyUser.Uses(webApplication, "", "");
            
            //landing page
            adminUser.Uses(landingPage, "", "");
            designUser.Uses(landingPage, "", "");
            buyUser.Uses(landingPage, "", "");
            
            //bounded context
            
            Container buyerContext = telaxiaApp.AddContainer("BuyerContext", "Contexto de los compradores de diseños ");
            Container paymentContext = telaxiaApp.AddContainer("PaymentProcessContext", "Contexto de pago");
            Container designerContext = telaxiaApp.AddContainer("DesignerContext", "Contexto de los diseñadores");
            Container notifyContext = telaxiaApp.AddContainer("NotifyContext", "Contexto de notificacion a correos");
            Container saleAgreementContext = telaxiaApp.AddContainer("SaleAgreementContext", "Contexto del registro de pedidos ");


            //Api uses BC
            apiRest.Uses(buyerContext, "", "");
            apiRest.Uses(paymentContext, "", "");
            apiRest.Uses(designerContext, "", "");
            apiRest.Uses(notifyContext, "", "");
            apiRest.Uses(saleAgreementContext, "", "");
            //database
            designerContext.Uses(dataBase, "", "JDBC");
            buyerContext.Uses(dataBase, "", "JDBC");
            notifyContext.Uses(dataBase, "", "JDBC");
            paymentContext.Uses(dataBase, "", "JDBC");
            saleAgreementContext.Uses(dataBase, "", "JDBC");
            
            //app use aditionals
            paymentContext.Uses(payU, "API Request", "JSON/HTTPS");
            notifyContext.Uses(gmail, "API Request", "JSON/HTTPS");
           
            

            // Tags
            mobileApplication.AddTags("MobileApp");
            webApplication.AddTags("WebApp");
            landingPage.AddTags("LandingPage");
            apiRest.AddTags("APIRest");
            dataBase.AddTags("Database");
            
            designerContext.AddTags("DesignerContext");
            buyerContext.AddTags("BuyerContext");
            notifyContext.AddTags("NotifyContext");
            paymentContext.AddTags("PaymentProcessContext");
            saleAgreementContext.AddTags("SaleAgreementContext");

            styles.Add(new ElementStyle("MobileApp") { Background = "#9d33d6", Color = "#ffffff", Shape = Shape.MobileDevicePortrait, Icon = "" });
            styles.Add(new ElementStyle("WebApp") { Background = "#9d33d6", Color = "#ffffff", Shape = Shape.WebBrowser, Icon = "" });
            styles.Add(new ElementStyle("LandingPage") { Background = "#929000", Color = "#ffffff", Shape = Shape.WebBrowser, Icon = "" });
            styles.Add(new ElementStyle("APIRest") { Shape = Shape.RoundedBox, Background = "#0000ff", Color = "#ffffff", Icon = "" });
            styles.Add(new ElementStyle("Database") { Shape = Shape.Cylinder, Background = "#ff0000", Color = "#ffffff", Icon = "" });
            
            styles.Add(new ElementStyle("DesignerContext") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("BuyerContext") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("NotifyContext") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("PaymentProcessContext") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("SaleAgreementContext") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });

            ContainerView containerView = viewSet.CreateContainerView(telaxiaApp, "Contenedor", "Diagrama de contenedores");
            contextView.PaperSize = PaperSize.A4_Landscape;
            containerView.AddAllElements();
            
            //Components
            //notify context
            Component emailComponent = apiRest.AddComponent("EmailComponent", "Envia mensaje a los usuarios");
            //designer context
            Component postComponent = apiRest.AddComponent("PostComponent", "Postea un diseño");
            Component postSummaryComponent = apiRest.AddComponent("postSummaryComponent", "Provee el registro de posts publicos y preivados");
            Component collabComponent = apiRest.AddComponent("CollabComponent", "Permite colaborar con otros diseñadores");
            Component accountDesignerController = apiRest.AddComponent("AccountDesignerController", "Modifica informacion de la propia cuenta(diseñador)");
            //buyer context
            Component commentComponent = apiRest.AddComponent("CommentComponent", "Sube un comentario hacia una publicacion ");
            Component ratingComponent = apiRest.AddComponent("RatingComponent", "Puntua un proyecto o diseño");
            Component shareComponent = apiRest.AddComponent("ShareComponent", "Comparte un proyecto o diseño");
            Component accountBuyerController = apiRest.AddComponent("AccountBuyerController", "Modifica informacion de la propia cuenta(comprador)");
            //payment context
            Component securityComponent = apiRest.AddComponent("SecurityComponent", "Provee funcionalidad relacionada a la verificacion de pagos y usuarios");
            Component payController = apiRest.AddComponent("PayController", "Realiza y verifica el pago del plan");
           //saleAgreement context
           Component saleAgreementSummaryController = apiRest.AddComponent("SaleAgreementSummaryController", "Provee el registro de diseños cedidos, acordado por ambas partes ");
           Component shareContactComponent = apiRest.AddComponent("SaleAgreementComponent", "Intercambia informacion de contacto");
            
           //USES
           mobileApplication.Uses(accountBuyerController,"","");
           mobileApplication.Uses(payController,"","");
           mobileApplication.Uses(saleAgreementSummaryController,"","");
           mobileApplication.Uses(accountDesignerController,"","");

           webApplication.Uses(accountBuyerController,"","");
           webApplication.Uses(payController,"","");
           webApplication.Uses(saleAgreementSummaryController,"","");
           webApplication.Uses(accountDesignerController,"","");
           //
           saleAgreementSummaryController.Uses(shareContactComponent, "", "");
          // accountBuyerController uses
          accountBuyerController.Uses(commentComponent, "", "");
          accountBuyerController.Uses(ratingComponent, "", "");
          accountBuyerController.Uses(shareComponent, "", "");
          //accountDesignerController uses
          accountDesignerController.Uses(postSummaryComponent, "", "");
          accountDesignerController.Uses(collabComponent, "", "");
          //postSummaryComponent
          postSummaryComponent.Uses(postComponent, "", "");
          //payController
          payController.Uses(securityComponent, "", "");
          payController.Uses(emailComponent, "", "");
          //others that use email
          shareContactComponent.Uses(emailComponent, "", "");
           
          //uses database
          securityComponent.Uses(dataBase, "", "JDBC");
          postSummaryComponent.Uses(dataBase, "", "JDBC");
          
          //others
          emailComponent.Uses(gmail, "", "");
          securityComponent.Uses(payU, "", "");
            
          //tags
          
          emailComponent.AddTags("EmailComponent");
          postComponent.AddTags("PostComponent");
          postSummaryComponent.AddTags("PostSummaryComponent");
          collabComponent.AddTags("CollabComponent");
          accountDesignerController.AddTags("AccountDesignerController");
          commentComponent.AddTags("CommentComponent");
          ratingComponent.AddTags("RatingComponent");
          shareComponent.AddTags("ShareComponent");
          accountBuyerController.AddTags("AccountBuyerController");
          securityComponent.AddTags("SecurityComponent");
          payController.AddTags("PayController");
          saleAgreementSummaryController.AddTags("SaleAgreementSummaryController");
          shareContactComponent.AddTags("ShareContactComponent");

        //styles
        styles.Add(new ElementStyle("EmailComponent") { Shape = Shape.Component,Color = "#ffffff", Background = "#555555", Icon = "" });
        styles.Add(new ElementStyle("PostComponent") { Shape = Shape.Component,Color = "#ffffff", Background = "#555555", Icon = "" });
        styles.Add(new ElementStyle("PostSummaryComponent") { Shape = Shape.Component,Color = "#ffffff", Background = "#555555", Icon = "" });
        styles.Add(new ElementStyle("CollabComponent") { Shape = Shape.Component,Color = "#ffffff", Background = "#555555", Icon = "" });
        styles.Add(new ElementStyle("AccountDesignerController") { Shape = Shape.Component,Color = "#ffffff", Background = "#555555", Icon = "" });
        styles.Add(new ElementStyle("CommentComponent") { Shape = Shape.Component,Color = "#ffffff", Background = "#555555", Icon = "" });
        styles.Add(new ElementStyle("RatingComponent") { Shape = Shape.Component,Color = "#ffffff", Background = "#555555", Icon = "" });
        styles.Add(new ElementStyle("ShareComponent") { Shape = Shape.Component,Color = "#ffffff", Background = "#555555", Icon = "" });
        styles.Add(new ElementStyle("AccountBuyerController") { Shape = Shape.Component,Color = "#ffffff", Background = "#555555", Icon = "" });
        styles.Add(new ElementStyle("SecurityComponent") { Shape = Shape.Component, Color = "#ffffff", Background = "#555555", Icon = "" });
        styles.Add(new ElementStyle("PayController") { Shape = Shape.Component,Color = "#ffffff", Background = "#555555", Icon = "" });
        styles.Add(new ElementStyle("SaleAgreementSummaryController") { Shape = Shape.Component,Color = "#ffffff", Background = "#555555", Icon = "" });
        styles.Add(new ElementStyle("ShareContactComponent") { Shape = Shape.Component,Color = "#ffffff", Background = "#555555", Icon = "" });
        
        ComponentView componentView = viewSet.CreateComponentView(apiRest, "Component", "Diagrama de componentes");
        componentView.PaperSize = PaperSize.A3_Landscape;
        componentView.AddAllElements();
        componentView.Remove(designerContext);
        componentView.Remove(buyerContext);
        componentView.Remove(notifyContext);
        componentView.Remove(paymentContext);
        componentView.Remove(saleAgreementContext);

        structurizrClient.UnlockWorkspace(workspaceId);
            structurizrClient.PutWorkspace(workspaceId, workspace);
            
            
        }
    }
}