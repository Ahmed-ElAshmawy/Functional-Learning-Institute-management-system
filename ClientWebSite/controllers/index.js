 (function() {
     //console.log("hub");
     hub = $.connection.signalR;
     $.connection.hub.url = "http://localhost:9408/signalr";
     angular.module("module").controller("index", ["$scope", "$location", "auth",
         function($scope, $location, auth) {
             auth.isAllowed($location.absUrl().split('#')[1].split('/')[1].split("?")[0]);
             $scope.messages = [];
             $scope.notificationCount = 0;
             hub.client.newMessage = function(n) {
                 $scope.notificationCount++;
                 $scope.messages.push(n)
                 $scope.$apply();
             }
             $scope.show = false;

             //  hub.client.confirmClient = function(n) {
             //      console.log(n)
             //      $scope.notificationCount++;
             //      $scope.messages.push(n)
             //      $scope.$apply();
             //  }

             hub.client.sync = function(p) {
                 console.log(p)
                 $scope.notificationCount = p.length;
                 $scope.messages = p;
                 $scope.$apply();
             }
             $scope.in = () => {
                 if (auth.isLoggedIn()) {
                     $.connection.hub.start().done(() => {
                         hub.server.newConnection(auth.getUserId());
                     });
                 }
             }
             $scope.in();
             $scope.isLoggedIn = auth.isLoggedIn();
             $scope.logOut = () => {
                 //call api
                 auth.logOut();
                 $scope.isLoggedIn = auth.isLoggedIn();
                 $location.path("/index");
                 $scope.roles = auth.getRoles();
                 $.connection.hub.stop()
             }
             $scope.$on("$routeChangeSuccess", function() {
                 console.log('routeChangeSuccess');
                 console.log(auth.getRoles());
                 $scope.pageName = ($location.$$path).substring(1);
                 $scope.isLoggedIn = auth.isLoggedIn();
                 $scope.roles = auth.getRoles();
                 $scope.show = auth.isLoggedIn()
             });
             $scope.$on("$locationChangeStart", function(event, newUrl, oldUrl) {
                 //  if (newUrl.split('#')[1].split('/')[1].split("?")[0] == 'notifications' && oldUrl.split('#')[1].split('/')[1].split("?")[0] == 'updatesession') {
                 //      $scope.showbtn = false;
                 //  } else
                 //      $scope.showbtn = true;
                 auth.isAllowed(newUrl.split('#')[1].split('/')[1].split("?")[0]);

             });
             $scope.details = (m) => {
                 console.log(m)
                 if ($scope.notificationCount > 0)
                     $scope.notificationCount--;
                 $scope.messages.splice($scope.messages.indexOf(m), 1);
                 console.log(m)
                 if (m.MessageBody == "New complaint")
                     $location.path('/Allcomplaint/' + m.Data);
                 else if (m.MessageBody.indexOf("complaint") != -1)
                     $location.path('/complaintdetails/' + m.Data);
                 else
                     $location.path('/notifications/' + m.Id + "/" + m.Data)

             }
         }
     ]);

 })();