angular.module("module", ["ngRoute", 'ngResource', 'ngCookies', 'SignalR']);
angular.module("module").config(function ($routeProvider, $locationProvider, $httpProvider) {
    $routeProvider
        .when("/home", {
            templateUrl: "views/home.html",
            controller: "home"
        })
        .when("/login", {
            templateUrl: "views/login.html",
            controller: "login"
        })
        .when("/parent", {
            templateUrl: "views/parent/index.html",
            controller: "parent"
        })
        .when("/child", {
            templateUrl: "views/child/index.html",
            controller: "child"
        })
        .when("/childschedule/:id", {
            templateUrl: "views/child/schedule.html",
            controller: "childschedule"
        })
        .when("/createchild", {
            templateUrl: "views/child/create.html",
            controller: "createchild"
        })
        .when("/updatechild", {
            templateUrl: "views/child/update.html",
            controller: "updatechild"
        })
        .when("/createparent", {
            templateUrl: "views/parent/create.html",
            controller: "createparent"
        })
        .when("/updateparent/:id", {
            templateUrl: "views/parent/update.html",
            controller: "updateparent"
        })
        .when("/createTherapistAvailability", {
            templateUrl: "views/therapist/createAvailability.html",
            controller: "createTherapistAvailability"
        })
        .when("/createChildAvailability/:id", {
            templateUrl: "views/child/createAvailability.html",
            controller: "createChildAvailability"
        })
        .when("/childAvailability/:id", {
            templateUrl: "views/child/availability.html",
            controller: "childAvailability"
        })
        .when("/therapistAvailability", {
            templateUrl: "views/therapist/availability.html",
            controller: "therapistAvailability"
        })
        .when("/therapist", {
            templateUrl: "views/therapist/index.html",
            controller: "therapist"
        })
        .when("/therapistDetails/:Id", {
            templateUrl: "views/therapist/details.html",
            controller: "therapistDetails"
        })
        .when("/createTherapist", {
            templateUrl: "views/therapist/create.html",
            controller: "createTherapist"
        })
        .when("/editTherapist/:Id", {
            templateUrl: "views/therapist/update.html",
            controller: "editTherapist"
        })
        .when("/session", {
            templateUrl: "views/session/index.html",
            controller: "session"
        })
        .when("/therapistschedule", {
            templateUrl: "views/therapist/schedule.html",
            controller: "schedule"
        })
        .when("/sessiondetails/:Id", {
            templateUrl: "views/session/detail.html",
            controller: "sessiondetail"
        })
        .when("/updatesession/:Id/:type?", {
            templateUrl: "views/session/update.html",
            controller: "updateSession"
        })
        .when("/assigntogroup/:Id", {
            templateUrl: "views/session/assignToGroup.html",
            controller: "assigntogroup"
        })
        .when("/newSession", {
            templateUrl: "views/session/create.html",
            controller: "newSession"
        })
        .when("/unauthorized", {
            templateUrl: "views/unauthorized.html",
            controller: "unauthorized"
        })
        .when("/parentprofile", {
            templateUrl: "views/parent/parentProfile.html",
            controller: "parentprofile"
        })
        .when("/recover", {
            templateUrl: "views/resetpassword.html",
            controller: "resetpassword"
        })
        .when("/forgetPassword", {
            templateUrl: "views/forgetPassword.html",
            controller: "forgetPassword"
        })
        .when("/profile", {
            templateUrl: "views/profile/index.html",
            controller: "profile"
        })
        .when("/profile/changeemail", {
            templateUrl: "views/profile/changeemail.html",
            controller: "changeemail"
        })
        .when("/profile/changepassword", {
            templateUrl: "views/profile/changepassword.html",
            controller: "changepassword"
        })
        .when("/contacts/:id", {
            templateUrl: "views/contact/index.html",
            controller: "contacts"
        })
        .when("/addRate/:id/:name", {
            templateUrl: "views/therapist/addRate.html",
            controller: "addRate"
        })
        .when("/createContact/:id", {
            templateUrl: "views/contact/create.html",
            controller: "createContact"
        })
        .when("/updateContact/:id", {
            templateUrl: "views/contact/update.html",
            controller: "updateContact"
        })
        .when("/parentemail/:id", {
            templateUrl: "views/emails/index.html",
            controller: "parentemail"
        })
        .when("/createparentemail/:id", {
            templateUrl: "views/emails/create.html",
            controller: "createparentemail"
        })
        .when("/updateparentemail/", {
            templateUrl: "views/emails/update.html",
            controller: "updateparentemail"
        })
        .when("/rateTherapist/:id/:name", {
            templateUrl: "views/therapist/rate.html",
            controller: "rateTherapist"
        })
        .when("/therapistTask", {
            templateUrl: "views/therapist/task.html",
            controller: "therapistTask"
        })
        .when("/updateTask", {
            templateUrl: "views/therapist/updateTask.html",
            controller: "updateTask"
        })
        .when("/createTask", {
            templateUrl: "views/therapist/createTask.html",
            controller: "createTask"
        })
        .when("/scheduleAll", {
            templateUrl: "views/schedule/index.html",
            controller: "scheduleAll"
        })
        .when("/RetrieveParent", {
            templateUrl: "views/parent/Retrieve.html",
            controller: "RetrieveParent"
        })
        .when("/retrieveTherapist", {
            templateUrl: "views/therapist/Retrieve.html",
            controller: "retrieveTherapist"
        })
        .when("/retrieveChild", {
            templateUrl: "views/child/Retrieve.html",
            controller: "retrieveChild"
        })
        .when("/retrieveContact", {
            templateUrl: "views/contact/Retrieve.html",
            controller: "retrieveContact"
        })
        .when("/complaint", {
            templateUrl: "views/complaint/complaint.html",
            controller: "complaint"
        })
        .when("/Allcomplaint/:id", {
            templateUrl: "views/complaint/index.html",
            controller: "Allcomplaint"
        })
        .when("/complaintdetails/:id", {
            templateUrl: "views/complaint/details.html",
            controller: "complaintDetails"
        })
        .when("/createcomplaint", {
            templateUrl: "views/complaint/create.html",
            controller: "createcomplaint"
        })
        .when("/notifications/:Id/:sessionId", {
            templateUrl: "views/notifications/index.html",
            controller: "notifications"
        })
        .when("/childrenSession", {
            templateUrl: "views/schedule/childSession.html",
            controller: "childSession"
        })
        .when("/reportView", {
            templateUrl: "views/Report/reportView.html",
            controller: "reportView"
        })
        .when("/therapistReport", {
            templateUrl: "views/Report/therapistReport.html",
            controller: "therapistReport"
        })
        .when("/parentReport/:childId", {
            templateUrl: "views/Report/parentReport.html",
            controller: "parentReport"
        })
        .when("/invoiceReport", {
            templateUrl: "views/Report/invoiceReport.html",
            controller: "invoiceReport"
        })
        .when("/invoicedownload/:childId", {
            templateUrl: "views/Report/invoicedownload.html",
            controller: "invoicedownload"
        })
        .when("/homeFundInvoice/:childId", {
            templateUrl: "views/Report/homeFundInvoice.html",
            controller: "homeFundInvoice"
        })
        .when("/generalInvoice/:childId/:fundType", {
            templateUrl: "views/Report/generalInvoice.html",
            controller: "generalInvoice"
        })
        .when("/therapistReportView", {
            templateUrl: "views/Report/therapistReportView.html",
            controller: "therapistReportView"
        })
        .when("/biMonthReport/:therapistId", {
            templateUrl: "views/Report/biMonthReport.html",
            controller: "biMonthReport"
        })
        .when("/endMonthReport/:therapistId", {
            templateUrl: "views/Report/endMonthReport.html",
            controller: "endMonthReport"
        })
        .when("/finalMonthReport/:therapistId", {
            templateUrl: "views/Report/finalMonthReport.html",
            controller: "finalMonthReport"
        })
        .when("/answercomplaint/:Id", {
            templateUrl: "views/complaint/answer.html",
            controller: "answercomplaint"
        })
        .when("/sessionFinshing", {
            templateUrl: "views/session/sessionFinshing.html",
            controller: "sessionFinshing"
        })
        .when("/updateTherapist/:id", {
            templateUrl: "views/therapist/update.html",
            controller: "updateTherapist"
        })
        .otherwise({
            redirectTo: "home",
            controller: "home"
        });
    $locationProvider.hashPrefix('');
});