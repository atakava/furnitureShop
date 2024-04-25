<script>
import axios from 'axios';

export default {
  name: "product-admin-page",
  data() {
    return {
      product: null,
      productUpdate: null,
      productId: null,
      paramterId: null,
      showModal: false,
      showEditModal: false
    }
  },
  methods: {
    async fetchProduct(id) {
      try {
        const response = await axios.post(
            "http://localhost:5199/api/Product/Get",
            {id: id}, {
              withCredentials: true,
              headers: {
                'Content-Type': 'application/json'
              }
            });

        console.log(response.data);

        this.product = response.data;
        this.productUpdate = this.product;

        console.log(this.productUpdate);

      } catch (e) {
        console.error("ошибка получения товарa", e);
        throw e;
      }
    },
    async addParameter() {
      try {
        const response = await axios.post(
            "http://localhost:5199/api/Product/AddParameterToProduct",
            {
              productId: this.productId,
              parameterId: this.paramterId
            }, {
              withCredentials: true,
              headers: {
                'Content-Type': 'application/json'
              }
            });

        await this.fetchProduct(this.productId);
        this.showModal = false;

      } catch (e) {
        console.error("ошибка добавления параметра", e);
        throw e;
      }
    },
    async removeParameter(parameterId) {
      try {
        const response = await axios.post(
            "http://localhost:5199/api/Product/RemoveParameterFromProduct",
            {
              productId: this.productId,
              parameterId: parameterId
            }, {
              withCredentials: true,
              headers: {
                'Content-Type': 'application/json'
              }
            });

        await this.fetchProduct(this.productId);

      } catch (e) {
        console.error("ошибка удаления параметра", e);
        throw e;
      }
    },
    async updateProduct() {
      try {

        const formData = new FormData();
        formData.append('id', this.productUpdate.id);
        formData.append('title', this.productUpdate.title);
        formData.append('price', parseInt(this.productUpdate.price));
        formData.append('count', parseInt(this.productUpdate.count));
        formData.append('shortDescription', this.productUpdate.shortDescription);
        formData.append('description', this.productUpdate.description);
        formData.append('catalogId', parseInt(this.productUpdate.catalogId));
        formData.append('image', this.productUpdate.image);

        const response = await axios.post(
            "http://localhost:5199/api/Product/Update",
            formData
            , {
              withCredentials: true,
              headers: {
                'Content-Type': 'multipart/form-data'
              }
            }
        );

        console.log(response.data);

        await this.fetchProduct(this.productId);
        this.showEditModal = false;

      } catch (e) {
        console.error("ошибка обновления продукта", e);
        throw e;
      }
    },
    handleImageChange(event) {
      const file = event.target.files[0];
      if (file) {
        this.productUpdate.image = file;
      }
    },
  },
  async created() {
    this.productId = this.$route.params.id;
    console.log(this.productId);

    await this.fetchProduct(this.productId);
  }
}
</script>

<template>
  <div class="container" v-if="product">
    <div class="title">{{ product.title }}</div>
    <img class="img" :src="'http://' + product.image" alt="img"/>
    <div class="price">Цена: {{ product.price }}</div>
    <div class="count">Количетсво: {{ product.count }}</div>
    <div class="shortDescription">Короткое Описания: {{ product.shortDescription }}</div>
    <div class="description">Полное Описание: {{ product.description }}</div>
    <div class="catalog">Каталог: {{ product.catalog.title }}</div>


    <div class="parameters" v-if="product.productParameters && product.productParameters.length > 0">
      <table class="parameters-table">
        <thead>
        <tr>
          <th>ID</th>
          <th>Название параметра</th>
          <th>Значение</th>
          <th>Удалить</th>
        </tr>
        </thead>
        <tbody>
        <tr v-for="item in product.productParameters" :key="item.id">
          <td>{{ item.id }}</td>
          <td>{{ item.name }}</td>
          <td>{{ item.value }}</td>
          <td>
            <button class="delete-parameter" @click="removeParameter(item.id)">
              <img src="/icons/clear.svg" alt="delete">
            </button>
          </td>
        </tr>
        </tbody>
      </table>
    </div>

    <div v-else>
      параметров нет
    </div>

    <form class="popup" @submit.prevent="addParameter" v-if="showModal">
      <div class="close" @click="showModal = false">
        <img src="/icons/clear.svg" alt="close">
      </div>
      <div class="popup-title">
        Добавление параметров
      </div>
      <div class="popup-item">
        <div class="product-id">ID параметра</div>
        <input v-model="paramterId">
      </div>
      <button class="submit">Добавить</button>
    </form>

    <button class="add-parameter" @click="showModal = true">Добавление параметров</button>

    <form class="popup" @submit.prevent="updateProduct" v-if="showEditModal">
      <div class="close" @click="showEditModal = false">
        <img src="/icons/clear.svg" alt="close">
      </div>
      <div class="popup-title">
        Изменение продукта
      </div>
      <div class="popup-item">
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
      </div>
      <button class="submit">Изменить</button>
    </form>

    <button class="edit-product" @click="showEditModal = true">
      Редактировать продукт
    </button>

  </div>
</template>

<style scoped>
.container {
  padding: 20px 60px;
  width: 1200px;
  margin: 20px 60px;
  border: 1px solid #ccc;
  border-radius: 8px;
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
}

.title {
  font-size: 24px;
  font-weight: bold;
  margin-bottom: 10px;
}

.img {
  width: 100%;
  max-height: 400px;
  object-fit: cover;
  margin-bottom: 10px;
}

.price, .count, .shortDescription, .description, .catalog {
  margin-bottom: 10px;
}

.parameters {
  margin-top: 20px;
}

.parameters-table {
  width: 100%;
  border-collapse: collapse;
  margin-top: 10px;
}

.parameters-table th, .parameters-table td {
  border: 1px solid #ddd;
  padding: 8px;
  text-align: left;
}

.delete-parameter:hover {
  background-color: #d32f2f;
}

.delete-parameter {
  width: 30px;
  height: 30px;
  background-color: #fe696a;
  display: flex;
  align-items: center;
  justify-content: center;
}

.delete-parameter img {
  width: 30px;
  height: 30px;
}

.add-parameter {
  background-color: #4caf50;
  color: white;
  border: none;
  padding: 10px 15px;
  border-radius: 4px;
  cursor: pointer;
  font-size: 16px;
  margin-top: 15px;
  margin-right: 35px;
}

.add-parameter:hover {
  background-color: #45a049;
}

.popup {
  position: fixed;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  padding: 20px;
  background-color: white;
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
  z-index: 1000;
}

.close {
  width: 20px;
  height: 20px;
  background-color: #fe696a;
  cursor: pointer;
  position: absolute;
  top: 10px;
  right: 10px;
}

.close img {
  width: 100%;
  height: 100%;
}

.popup-title {
  font-size: 18px;
  font-weight: bold;
  margin-bottom: 10px;
}

.popup-item {
  margin-bottom: 15px;
}

.product-id {
  font-weight: bold;
  margin-bottom: 5px;
}

input {
  width: 100%;
  padding: 8px;
  box-sizing: border-box;
  margin-top: 5px;
  margin-bottom: 10px;
  border: 1px solid #ccc;
  border-radius: 4px;
  font-size: 14px;
}

.submit {
  background-color: #4caf50;
  color: white;
  padding: 10px 15px;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  font-size: 16px;
}

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

.submit:hover {
  background-color: #45a049;
}

.edit-product {
  background-color: #d7ad1b;
  color: white;
  border: none;
  padding: 10px 15px;
  border-radius: 4px;
  cursor: pointer;
  font-size: 16px;
  margin-top: 15px;
  margin-right: 35px;
}

</style>
