const identityRoutes = [
    {
        path: "/auth/login",
        name: "Login",
        component: () => import("../views/Login.vue"),
    },
    {
        path: "/auth/register",
        name: "Register",
        component: () => import("../views/Register.vue"),
    }
];

export default identityRoutes;
