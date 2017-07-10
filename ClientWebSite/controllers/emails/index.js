angular.module("module").controller("parentemail", ["$scope", "getAjax", "$http", "$location", "serverUrl", "$routeParams",
    function($scope, getAjax, $http, $location, serverUrl, $routeParams) {
        console.log("Hello From parentemail");
        $scope.parentId = $routeParams.id;
        $scope.isLoading = true;
        getemails = () => {
            getAjax.response(serverUrl + "api/ParentEmail/GetByparent?parentid=" + $scope.parentId).
            then((data) => {
                console.log(data.data);
                $scope.emails = data.data;
                $scope.isLoading = false;
            }, (error) => {
                console.log(error);
            });
        }
        getemails()
        $scope.confirmdelete = (id) => {
            $scope.id = id;
            console.log("iddddddddd", $scope.id)
            $("#deletemodal").modal('toggle');
        }
        $scope.delete = () => {
            console.log($scope.id);
            $http.delete(serverUrl + "api/ParentEmail/" + $scope.id).
            then((response) => {
                console.log(response);
                $("#deletemodal").toggle();
                $('.in').hide();
                getemails();
            }, (error) => {
                console.log(error);
            });
        }
        $scope.showcreate = () => {
            $location.path('/createparentemail/' + $scope.parentId)
        }
        $scope.showupdate = (email, id) => {
            console.log("email", email);
            console.log("id", id);
            $location.path('/updateparentemail/').search({
                Id: id,
                parentId: $scope.parentId,
                parentEmail: email
            })
        }

    }
])