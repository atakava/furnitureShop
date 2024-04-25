<script>
import ProductRow from "@/components/product-row.vue";
import BanerElement from "@/components/baner-element.vue";

import axios from 'axios';

export default {
  name: "product-grid",
  components: {BanerElement, ProductRow},
  data() {
    return {
      catalog: null,
      banners: null,
      cabinets: null
    }
  },
  methods: {
    async fetchBanners() {
      try {
        const response = await axios.post(
            "http://localhost:5199/api/Banner/GetAll",
            null,
            {
              withCredentials: true,
              headers: {
                'Content-Type': 'application/json'
              }
            }
        );

        this.banners = response.data.data;

      } catch (e) {
        console.error("ошибка получения банеров", e);
        throw e;
      }
    },
    getLastBanner() {
      return this.banners.length > 0 ? this.banners[this.banners.length - 1] : null;
    },
    async fetchCatalog() {
      try {
        const response = await axios.post(
            "http://localhost:5199/api/Catalog/GetAll",
            null,
            {
              withCredentials: true,
              headers: {
                'Content-Type': 'application/json'
              }
            }
        );

        this.catalog = response.data;

      } catch (e) {
        console.error("ошибка полученя каталогов", e);
        throw e;
      }
    },
    filteredCatalogs(type) {
      return this.catalog.filter(catalog => catalog.title.includes(type));
    },
  },
  async mounted() {
    await this.fetchBanners();
    await this.fetchCatalog();
  }
}
</script>

<template>
  <div class="product-grid" v-if="catalog && banners">
    <div class="product-row" v-for="item in filteredCatalogs('Диваны')">
      <div class="title">{{ item.title }}</div>
      <div class="products">
        <product-row :data="item.product.slice(-5)"/>
      </div>
    </div>
    <baner-element :banner="getLastBanner()"/>
    <div class="product-row" v-for="item in filteredCatalogs('Шкафы')">
      <div class="title">{{ item.title }}</div>
      <div class="products">
        <product-row :data="item.product.slice(-5)"/>
      </div>
    </div>
    <div class="product-row" v-for="item in filteredCatalogs('Кухни')">
      <div class="title">{{ item.title }}</div>
      <div class="products">
        <product-row :data="item.product.slice(-5)"/>
      </div>
    </div>
  </div>
</template>

<style scoped>
.title {
  font-size: 24px;
  font-weight: 500;
  cursor: pointer;
  margin-bottom: 25px;
}

.products {
  display: flex;
  justify-content: space-between;
}

.product-row {
  margin: 75px 0 75px 0;
}

</style>