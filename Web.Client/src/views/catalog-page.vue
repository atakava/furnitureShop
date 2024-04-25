<script>
import ProductCard from "@/components/product-card.vue";
import axios from 'axios';

export default {
  name: "catalog-page",
  components: {ProductCard},
  data() {
    return {
      catalogProducts: [],
      catalog: null,
    }
  },
  methods: {
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

        console.log()
        this.catalog = response.data;

      } catch (e) {
        console.error("Ошибка получения каталогов", e);
        throw e;
      }
    },
    async fetchCatalogByTitle(title) {
      try {
        const response = await axios.post(
            "http://localhost:5199/api/Catalog/GetByTitle",
            {
              title: title
            },
            {
              withCredentials: true,
              headers: {
                'Content-Type': 'application/json'
              }
            }
        );

        console.log()
        this.catalogProducts = response.data.product;

      } catch (e) {
        console.error("Ошибка получения продуктов для каталога", e);
        throw e;
      }
    },
    async fetchProducts() {
      try {
        const response = await axios.post(
            "http://localhost:5199/api/Product/GetAll",
            null, {
              withCredentials: true,
              headers: {
                'Content-Type': 'application/json'
              }
            });

        this.catalogProducts = response.data.products;

      } catch (e) {
        console.error("ошибка получения товаров", e);
        throw e;
      }
    },
  },
  async created() {
    await this.fetchCatalog();
    await this.fetchProducts();
  }
}
</script>
<template>
  <section class="wrap page" v-if="catalog">
    <div class="filter">
      <div class="text">
        Каталог
      </div>
      <div class="item" v-for="item in catalog" :key="item.id" @click="fetchCatalogByTitle(item.title)">
        <div>{{ item.title }}</div>
      </div>
    </div>
    <div>
      <div class="grid-product" v-if="catalogProducts.length > 0">
        <product-card class="product" :product="product" v-for="product in catalogProducts"
                      :key="product.id"></product-card>
      </div>
      <div v-else>
        товаров в данном каталоге пока что не существует :(
      </div>
    </div>
  </section>
</template>

<style scoped>
.page {
  display: flex;
  justify-content: space-between;
  padding: 35px 0;
}

.filter {
  width: 300px;
  height: 100%;
  box-shadow: 0px 0px 19px -4px rgba(0, 0, 0, 0.75);
  border-radius: 14px;
  padding: 25px;
}

.filter .text {
  font-size: 26px;
  margin-bottom: 25px;
}

.item {
  cursor: pointer;
  font-size: 18px;
  margin-bottom: 15px;
  transition: color 0.3s ease;
}

.item:hover {
  color: #fe696a;
}

.grid-product {
  display: grid;
  grid-template-columns: repeat(4, 1fr);
  column-gap: 25px;
  row-gap: 25px;
}

.product {
  margin-bottom: 50px;
}

</style>