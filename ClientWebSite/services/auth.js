angular.module("module").factory("auth", ["$cookies", "$location", "pages",
    function($cookies, $location, pages) {
        return {
            set: function(credential) {
                var expireDate = new Date(credential[".expires"]);
                //$cookies.putObject('credentials', credential, { 'expires': expireDate, secure: true });
                $cookies.putObject('credentials', credential, { 'expires': expireDate });
            },
            setAll: function(credential, roles, userId) {
                console.log("test roles", roles);
                var expireDate = new Date(credential[".expires"]);
                $cookies.putObject('userId', userId, { 'expires': expireDate });
                $cookies.putObject('roles', roles, { 'expires': expireDate });
                $cookies.putObject('credentials', credential, { 'expires': expireDate });
            },
            setRoles: function(roles) {
                console.log('set roles');
                console.log(roles);
                $cookies.putObject('roles', roles);
            },
            setUserId: function(id) {
                $cookies.putObject('userId', id);
            },
            getUserId: function(id) {
                var temp = $cookies.getObject('userId');
                if (temp == undefined) {
                    return { "": "" }
                } else {
                    return temp;
                }
            },
            setChildId: function(id) {
                $cookies.putObject('childId', id);
            },

            getChildId: () => {
                var temp = $cookies.getObject('childId');
                if (temp == undefined) {
                    return { "": "" }
                } else {
                    return temp;
                }
            },
            clearRoles: function() {
                $cookies.remove('roles');
            },
            remove: function() {
                $cookies.remove('roles');
                $cookies.remove('credentials');
            },
            getToken: () => {
                var temp = $cookies.getObject('credentials');
                if (temp == undefined) {
                    return { "": "" }
                } else {
                    return "bearer " + temp.access_token;
                }
            },
            getRoles: () => {
                var temp = $cookies.getObject('roles');
                if (temp == undefined) {
                    return ["Other"]
                } else {
                    return temp;
                }
            },
            getUserName: () => {
                var temp = $cookies.getObject('credentials');
                if (temp == undefined) {
                    return { "": "" }
                } else {
                    return temp.userName;
                }
            },
            getCredential: () => {
                var temp = $cookies.getObject('credentials');
                return temp;
            },
            isLoggedIn: () => {
                var temp = $cookies.getObject('credentials');
                if (temp == undefined)
                    return false;
                else
                    return true;
            },
            logOut: () => {
                $cookies.remove("credentials");
                $cookies.remove("roles");
                $cookies.remove("userId");
            },
            isAllowed: (route) => {

                console.log(route);
                var roles = $cookies.getObject('roles', roles); //array
                console.log(roles);
                if (route == "forgetPassword") {
                    $location.path("/forgetPassword");
                    return;
                }
                if (roles == undefined) {
                    $location.path("/login");
                    return;
                }
                var array = new Set(pages[roles[0]]);
                var ifHas = array.has(route); // true
                if (ifHas == false) {
                    $location.path("/unauthorized");
                    return;
                }
            }
        }
    }
]);