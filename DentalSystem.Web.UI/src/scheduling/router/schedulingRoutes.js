const schedulingRoutes = [
    {
        showOn: 'Authorized',
        path: "/scheduling/Schedule",
        name: "Schedule",
        component: () => import("../views/Schedule.vue"),
    }
];

export default schedulingRoutes;
