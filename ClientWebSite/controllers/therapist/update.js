angular.module("module").controller("updateTherapist", ["$scope", "getAjax", "$http", "$location", "serverUrl", "$routeParams",
    function($scope, getAjax, $http, $location, serverUrl, $routeParams) {
        console.log("Enter update therapist page");
        $scope.therapistId = $routeParams.id
        console.log("therapist id", $scope.therapistId)
        getAjax.response(serverUrl + "api/therapists/" + $scope.therapistId)
            .then(function(data) {
                $scope.data = data.data;
                console.log($scope.data)
                $scope.birthDate = $scope.data.birthDate;
                $scope.fullLegalName = $scope.data.fullLegalName;
                $scope.lastName = $scope.data.lastName;
                $scope.sin = $scope.data.sin;
                $scope.crcExpiry = $scope.data.crcExpiry;
                $scope.staffName = $scope.data.staffName;
                $scope.isEmployee = $scope.data.isEmployee;
                $scope.jopTitle = $scope.data.jopTitle;
                $scope.startDate = $scope.data.startDate;
                $scope.terminationDate = $scope.data.terminationDate;
                $scope.institueNo = $scope.data.institueNo;
                $scope.transitNo = $scope.data.transitNo;
                $scope.email = $scope.data.email;
                $scope.phone = $scope.data.phone;
                $scope.postalCode = $scope.data.postalCode;
                $scope.emergancyContact = $scope.data.emergancyContact;
                $scope.emergancyPhone = $scope.data.emergancyPhone;
                $scope.accountNo = $scope.data.accountNo;
                $scope.address = $scope.data.address;
            }, function(error) {});

        $scope.backToTherapist = function() {
            console.log("Enter click event to go back")
            $location.path("/therapist");
        }

        $scope.reset = () => {
            console.log("enter reset function");
            $scope.birthDate = $scope.data.birthDate;
            $scope.fullLegalName = $scope.data.fullLegalName;
            $scope.lastName = $scope.data.lastName;
            $scope.sin = $scope.data.sin;
            $scope.crcExpiry = $scope.data.crcExpiry;
            $scope.staffName = $scope.data.staffName;
            $scope.isEmployee = $scope.data.isEmployee;
            $scope.jopTitle = $scope.data.jopTitle;
            $scope.startDate = $scope.data.startDate;
            $scope.terminationDate = $scope.data.terminationDate;
            $scope.institueNo = $scope.data.institueNo;
            $scope.transitNo = $scope.data.transitNo;
            $scope.phone = $scope.data.phone;
            $scope.postalCode = $scope.data.postalCode;
            $scope.emergancyContact = $scope.data.emergancyContact;
            $scope.emergancyPhone = $scope.data.emergancyPhone;
            $scope.accountNo = $scope.data.accountNo;
            $scope.address = $scope.data.address;
            $scope.email = $scope.data.email;
        }


        $scope.edithTherapist = function() {
            console.log("enter clic event of edit therapist");
            $scope.therapistObject = {
                "id": $scope.therapistId,
                "birthDate": $scope.birthDate,
                "fullLegalName": $scope.fullLegalName,
                "lastName": $scope.lastName,
                "sin": $scope.sin,
                "crcExpiry": $scope.crcExpiry,
                "staffName": $scope.staffName,
                "isEmployee": $scope.isEmployee,
                "jopTitle": $scope.jopTitle,
                "startDate": $scope.startDate,
                "terminationDate": $scope.terminationDate,
                "institueNo": $scope.institueNo,
                "transitNo": $scope.transitNo,
                "phone": $scope.phone,
                "postalCode": $scope.postalCode,
                "emergancyContact": $scope.emergancyContact,
                "emergancyPhone": $scope.emergancyPhone,
                "address": $scope.address,
                "accountNo": $scope.accountNo,
                "email": $scope.email,
            };
            console.log("object of edit", $scope.therapistObject);
            var url = serverUrl + "api/therapists/" + $scope.therapistId;
            var data = $scope.therapistObject;
            var config = 'contenttype';
            $http.put(url, data, config).then(function(response) {
                console.log(response);
                if (response.status == 200) {
                    $location.path("/therapist");
                }
            }, function(error) {
                console.log(error);
            });
        }





    }
]);