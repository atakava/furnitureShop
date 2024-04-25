<script>
import axios from 'axios';

export default {
  name: "slider-panel",
  data() {
    return {
      slides: null,
      image: null,
      showModal: false
    }
  },
  methods: {
    handleImageChange(event) {
      const file = event.target.files[0];
      if (file) {
        this.image = file;
      }
    },
    async fetchSlides() {
      try {
        const response = await axios.post(
            "http://localhost:5199/api/Slider/GetAll",
            null,
            {
              withCredentials: true,
              headers: {
                'Content-Type': 'application/json'
              }
            }
        );

        this.slides = response.data.data;

      } catch (e) {
        console.error("ошибка получения слайдов", e);
        throw e;
      }
    },
    async createSlide() {
      try {

        const formData = new FormData();
        formData.append('image', this.image);

        const response = await axios.post(
            "http://localhost:5199/api/Slider/Create",
            {image: this.image}, {
              withCredentials: true,
              headers: {
                'Content-Type': 'multipart/form-data'
              }
            });

        await this.fetchSlides();
        this.showModal = false;

      } catch (e) {
        console.error("ошибка удаленя слайда", e);
        throw e;
      }
    },
    async deleteSlide(id) {
      try {
        const response = await axios.post(
            "http://localhost:5199/api/Slider/Delete",
            {id: id}, {
              withCredentials: true,
              headers: {
                'Content-Type': 'application/json'
              }
            });

        await this.fetchSlides();

      } catch (e) {
        console.error("ошибка удаленя слайда", e);
        throw e;
      }
    },
  },
  async created() {
    await this.fetchSlides();
  }
}
</script>

<template>
  <div class="container">
    <button class="create" @click="showModal = true">Создать</button>

    <form class="popup" @submit.prevent="createSlide" v-if="showModal">
      <div class="close" @click="showModal = false">
        <img src="/icons/clear.svg" alt="close">
      </div>
      <div class="title">Создание слайда</div>
      <div class="item">
        <div class="name">Изображение</div>
        <input type="file" @change="handleImageChange">
      </div>
      <button type="submit">Создать</button>
    </form>

    <table v-if="slides">
      <thead>
      <tr>
        <th>ID</th>
        <th>Слайд</th>
        <th>Удаление</th>
      </tr>
      </thead>
      <tbody>
      <tr v-for="item in slides">
        <td>{{ item.id }}</td>
        <td>
          <img alt="slide img" :src="'http://' + item.imagePath">
        </td>
        <td>
          <button class="delete" @click="deleteSlide(item.id)">
            <img src="/icons/clear.svg" alt="clear">
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

.popup {
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  background-color: #fff;
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
  border-radius: 8px;
  overflow: hidden;
  width: 300px;
}

.title {
  background-color: #3bb07c;
  color: #fff;
  padding: 10px;
  font-weight: bold;
  text-align: center;
}

.item {
  padding: 10px;
}

.name {
  font-weight: bold;
  margin-bottom: 5px;
}

input[type="file"] {
  width: 100%;
  padding: 8px;
  box-sizing: border-box;
  margin-top: 5px;
  margin-bottom: 10px;
  border: 1px solid #ccc;
  border-radius: 4px;
  font-size: 14px;
}

.popup button {
  background-color: #3bb07c;
  color: #fff;
  padding: 10px;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  font-size: 16px;
  width: 100%;
}

</style>