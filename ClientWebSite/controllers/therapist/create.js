angular.module("module").controller("createTherapist", ["$scope", "getAjax", "$http", "$location", "serverUrl",
    function($scope, getAjax, $http, $location, serverUrl) {
        console.log("Enter create therapist page");
        $scope.reset = () => {
            console.log("enter reset function");
            $scope.BirthDate = "";
            $scope.FullLegalName = "";
            $scope.LastName = "";
            $scope.SIN = "";
            $scope.UserName = "";
            $scope.CRCExpiry = "";
            $scope.StaffName = "";
            $scope.JopTitle = "";
            $scope.StartDate = "";
            $scope.InstitueNo = "";
            $scope.TransitNo = "";
            $scope.Email = "";
            $scope.Phone = "";
            $scope.PostalCode = "";
            $scope.EmergancyContact = "";
            $scope.EmergancyPhone = "";
            $scope.AccountNo = "";
            $scope.Address = "";

        }

        $scope.submitTherapist = () => {
            console.log('enter submit ');
            getAjax.postForm(serverUrl + "api/therapists/", $("#createForm").serialize()
                //, {
                //         headers: {
                //             'content-type': 'application/x-www-form-urlencoded'
                //         }
                //     }
            ).then((response) => {
                console.log(response.data);
                $location.path("/therapist");
            }, (error) => { console.log(error); });
            console.log("seralize", $('#createForm').serialize());
        }

        $scope.backTherapist = function() {
            $location.path("/therapist");
        }
    }
]);