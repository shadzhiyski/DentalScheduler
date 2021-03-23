const identityRoutes = [
    {
        path: "/auth/login",
        name: "Login",
        component: () => import("../views/Login.vue"),
    }
];

export default identityRoutes;
