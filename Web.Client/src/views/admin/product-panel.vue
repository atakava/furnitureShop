<script>
import axios from "axios";

export default {
  name: "product-panel",
  data() {
    return {
      products: null
    }
  },
  methods: {
    async deleteProduct(id) {
      try {
        const response = await axios.post(
            "http://localhost:5199/api/Product/Delete",
            {id: id}, {
              withCredentials: true,
              headers: {
                'Content-Type': 'application/json'
              }
            });

        console.log(response.data.products);

        await this.fetchProduct();

      } catch (e) {
        console.error("ошибка удаленя товара", e);
        throw e;
      }
    },
    async fetchProduct() {
      try {
        const response = await axios.post(
            "http://localhost:5199/api/Product/GetAll",
            null, {
              withCredentials: true,
              headers: {
                'Content-Type': 'application/json'
              }
            });

        console.log(response.data.products);

        this.products = response.data.products;

      } catch (e) {
        console.error("ошибка получения товаров", e);
        throw e;
      }
    }
  },
  async mounted() {
    await this.fetchProduct();
  }
}
</script>

<template>
  <div class="container">
    <button class="create" @click="this.$router.push('/product-create')">Создать</button>

    <table v-if="products">
      <thead>
      <tr>
        <th>ID</th>
        <th>Изображение</th>
        <th>Назваение</th>
        <th>Каталог</th>
        <th>Цена</th>
        <th>Удаление</th>
      </tr>
      </thead>
      <tbody>
      <tr v-for="item in products">
        <td @click="this.$router.push('/product/admin/' + item.id)">{{ item.id }}</td>
        <td @click="this.$router.push('/product/admin/' + item.id)">
          <img :src="'http://' + item.image" alt="Product Image">
        </td>
        <td @click="this.$router.push('/product/admin/' + item.id)">{{ item.title }}</td>
        <td @click="this.$router.push('/product/admin/' + item.id)">{{ item.catalog.title }}</td>
        <td @click="this.$router.push('/product/admin/' + item.id)">{{ item.price }}</td>
        <td>
          <button class="delete" @click="deleteProduct(item.id)">
            <img src="/icons/clear.svg" alt="delete">
          </button>
        </td>
      </tr>
      </tbody>
    </table>
  </div>
</template>

<style scoped>
.container {
  padding: 0 90px;
  width: 1300px;
}

.create {
  margin-top: 10px;
  margin-bottom: 25px;
  width: 100%;
  height: 35px;
  font-size: 20px;
  font-weight: 500;
  color: #fff;
  background-color: #3bb07c;
}

img {
  max-width: 50px;
  max-height: 50px;
}

</style>