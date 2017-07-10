angular.module("module").factory("getAjax", ["$http", "auth", "$resource", function($http, auth, $resource) {
    return {
        response: function(url) {
            return $http.get(url, {
                headers: {
                    'Authorization': auth.getToken()
                }
            });
        },
        get: function(url) {
            return $http.get(url, {
                headers: {
                    'Authorization': auth.getToken()
                }
            });
        },
        postForm: function(url, data) {
            return $http.post(url, data, {
                headers: {
                    'content-type': 'application/x-www-form-urlencoded',
                    'Authorization': auth.getToken()
                }
            });
        },
        postJson: function(url, data) {
            return $http.post(url, data, {
                headers: {
                    'Content-Type': "application/json",
                    'Authorization': auth.getToken()
                }
            });
        },
        putJson: function(url, data) {
            return $http.put(url, data, {
                headers: {
                    'Content-Type': "application/json",
                    'Authorization': auth.getToken()
                }
            });
        },
        putForm: function(url, data) {
            return $http.put(url, data, {
                headers: {
                    'content-type': 'application/x-www-form-urlencoded',
                    'Authorization': auth.getToken()
                }
            });
        },
        delete: function(url) {
            return $http.delete(url, {
                headers: {
                    'Authorization': auth.getToken()
                }
            });
        },
    }
}]);