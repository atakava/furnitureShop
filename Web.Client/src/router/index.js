import {createRouter, createWebHistory} from 'vue-router'
import mainPage from "@/views/main-page.vue";
import mainAdminPage from "@/views/admin/main-page.vue";
import catalogPage from "@/views/catalog-page.vue";
import productPage from "@/views/product-page.vue";
import mainLayout from "@/layouts/main-layout.vue";
import adminLayout from "@/layouts/admin-layout.vue";
import catalogPanel from "@/views/admin/catalog-panel.vue";
import productPanel from "@/views/admin/product-panel.vue";
import productCreate from "@/views/admin/product-create.vue";
import productAdminPage from "@/views/admin/product-admin-page.vue";
import parameterPanel from "@/views/admin/parameter-panel.vue";
import UserPanel from "@/views/admin/user-panel.vue";
import sliderPanel from "@/views/admin/slider-panel.vue";
import bannerPanel from "@/views/admin/banner-panel.vue";
import about from "@/components/about.vue";

const router = createRouter({
    history: createWebHistory(import.meta.env.BASE_URL),
    routes: [
        {
            alias: '/',
            path: '/main',
            name: 'main',
            component: mainPage,
            meta: {
                layout: mainLayout
            },
        },
        {
            path: '/catalog',
            name: 'catalog',
            component: catalogPage,
            meta: {
                layout: mainLayout
            },
        },
        {
            path: '/about',
            name: 'about',
            component: about,
            meta: {
                layout: mainLayout
            },
        },
        {
            path: '/product/:id',
            name: 'product',
            component: productPage,
            meta: {
                layout: mainLayout
            },
        },
        {
            path: '/admin-main',
            name: 'admin-main',
            component: mainAdminPage,
            meta: {
                layout: adminLayout
            },
        },
        {
            path: '/catalog-panel',
            name: 'catalog-panel',
            component: catalogPanel,
            meta: {
                layout: adminLayout
            },
        },
        {
            path: '/product-panel',
            name: 'product-panel',
            component: productPanel,
            meta: {
                layout: adminLayout
            },
        },
        {
            path: '/product-create',
            name: 'product-create',
            component: productCreate,
            meta: {
                layout: adminLayout
            },
        },
        {
            path: '/product/admin/:id',
            component: productAdminPage,
            meta: {
                layout: adminLayout,
            },
        },
        {
            path: '/parameter-panel',
            name: 'parameter-panel',
            component: parameterPanel,
            meta: {
                layout: adminLayout
            },
        },
        {
            path: '/user-panel',
            name: 'user-panel',
            component: UserPanel,
            meta: {
                layout: adminLayout
            },
        },
        {
            path: '/slider-panel',
            name: 'slider-panel',
            component: sliderPanel,
            meta: {
                layout: adminLayout
            },
        },
        {
            path: '/banner-panel',
            name: 'banner-panel',
            component: bannerPanel,
            meta: {
                layout: adminLayout
            },
        },
    ]
})

export default router
