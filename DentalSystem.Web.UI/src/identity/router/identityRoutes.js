const identityRoutes = [
    {
        showOn: 'NonAuthorized',
        path: "/auth/login",
        name: "Login",
        component: () => import("../views/Login.vue"),
    },
    {
        showOn: 'NonAuthorized',
        path: "/auth/register",
        name: "Register",
        component: () => import("../views/Register.vue"),
    },
    {
        showOn: 'Authorized',
        path: "/user/profile",
        name: "User Profile",
        component: () => import("../views/UserProfile.vue"),
    }
];

export default identityRoutes;
