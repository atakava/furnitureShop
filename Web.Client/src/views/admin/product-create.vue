<script>
import axios from "axios";

export default {
  name: "product-create",
  data() {
    return {
      product: {
        title: "",
        price: null,
        count: null,
        shortDescription: "",
        description: "",
        catalogId: null,
        image: null
      }
    }
  },
  methods: {
    handleImageChange(event) {
      const file = event.target.files[0];
      if (file) {
        this.product.image = file;
      }
    },
    async createProduct() {
      try {
        const formData = new FormData();
        formData.append('id', 1);
        formData.append('title', this.product.title);
        formData.append('price', parseInt(this.product.price));
        formData.append('count', parseInt(this.product.count));
        formData.append('shortDescription', this.product.shortDescription);
        formData.append('description', this.product.description);
        formData.append('catalogId', parseInt(this.product.catalogId));
        formData.append('image', this.product.image);

        const response = await axios.post(
            "http://localhost:5199/api/Product/Create",
            formData,
            {
              withCredentials: true,
              headers: {
                'Content-Type': 'multipart/form-data'
              }
            }
        );

        this.product = {
          title: "",
          price: null,
          count: null,
          shortDescription: "",
          description: "",
          catalogId: null,
          image: null
        }

        console.log(response);

        this.$router.push('/product-panel');

      } catch (e) {
        console.error("Ошибка при создании товара", e);
        console.log("Response data:", e.response.data);
        console.log("Response status:", e.response.status);
        console.log("Response headers:", e.response.headers);
        throw e;
      }
    }
  }
}
</script>

<template>
  <div class="container">
    <p class="title-page">Создание странички</p>
    <form @submit.prevent="createProduct">
      <div class="item">
        <div class="name">Название товара</div>
        <input v-model="product.title">
      </div>
      <div class="item">
        <div class="name">Цена</div>
        <input type="number" v-model="product.price">
      </div>
      <div class="item">
        <div class="name">Количество</div>
        <input type="number" v-model="product.count">
      </div>
      <div class="item">
        <div class="name">Короткое описание</div>
        <textarea v-model="product.shortDescription"/>
      </div>
      <div class="item">
        <div class="name">Полное описание</div>
        <textarea v-model="product.description"/>
      </div>
      <div class="item">
        <div class="name">ID каталога</div>
        <input type="number" v-model="product.catalogId">
      </div>
      <div class="item">
        <div class="name">Изображение</div>
        <input type="file" @change="handleImageChange">
      </div>

      <button class="submit" type="submit">Создать</button>
    </form>
  </div>
</template>

<style scoped>
.container {
  padding: 30px 90px;
  width: 1300px;
}


.title-page {
  font-size: 28px;
  font-weight: 600;
  margin-bottom: 50px;
}

body {
  font-family: Arial, sans-serif;
  background-color: #f4f4f4;
  margin: 0;
  padding: 0;
  display: flex;
  justify-content: center;
  align-items: center;
  height: 100vh;
}

form {
  background-color: #fff;
  padding: 20px;
  border-radius: 8px;
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
}

.item {
  margin-bottom: 15px;
}

.name {
  font-weight: bold;
  margin-bottom: 5px;
}

input,
textarea,
select {
  width: 100%;
  padding: 8px;
  box-sizing: border-box;
  margin-top: 5px;
  margin-bottom: 10px;
  border: 1px solid #ccc;
  border-radius: 4px;
  font-size: 14px;
}

input[type="file"] {
  margin-top: 10px;
}

button {
  background-color: #4CAF50;
  color: white;
  padding: 10px 15px;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  font-size: 16px;
}

button:hover {
  background-color: #45a049;
}
</style>