<script>
import axios from 'axios';

export default {
  name: "banner-panel",
  data() {
    return {
      banners: null,
      banner: {
        title: "",
        text: "",
        image: null,
        targetProductPath: "",
      },
      showModal: false
    }
  },
  methods: {
    handleImageChange(event) {
      const file = event.target.files[0];
      if (file) {
        this.banner.image = file;
      }
    },
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
    async createBanner() {
      try {

        const formData = new FormData();
        formData.append('title', this.banner.title);
        formData.append('text', this.banner.text);
        formData.append('image', this.banner.image);
        formData.append('targetProductPath', this.banner.targetProductPath);

        const response = await axios.post(
            "http://localhost:5199/api/Banner/Create",
            formData,
            {
              withCredentials: true,
              headers: {
                'Content-Type': 'multipart/form-data'
              }
            }
        );

        await this.fetchBanners();
        this.showModal = false;

      } catch (e) {
        console.error("ошибка созданя банеров", e);
        throw e;
      }
    },
    async deleteBanner(id) {
      try {

        const response = await axios.post(
            "http://localhost:5199/api/Banner/Delete",
            {id: id},
            {
              withCredentials: true,
              headers: {
                'Content-Type': 'application/json'
              }
            }
        );

        await this.fetchBanners();

      } catch (e) {
        console.error("ошибка удаления банера", e);
        throw e;
      }
    },
  },
  async created() {
    await this.fetchBanners();
  }
}
</script>

<template>
  <div class="container">
    <div class="pop-up" v-if="showModal">
      <form @submit.prevent="createBanner">
        <div class="close" @click="showModal = false">
          <img src="/icons/clear.svg" alt="close">
        </div>
        <div class="title-create">Создание баннера</div>
        <div class="pop-up-item">
          <div class="name">Название</div>
          <input type="text" v-model="banner.title">
        </div>
        <div class="pop-up-item">
          <div class="name">Текст</div>
          <textarea v-model="banner.text"></textarea>
        </div>
        <div class="pop-up-item">
          <div class="name">Изображение</div>
          <input type="file" @change="handleImageChange">
        </div>
        <div class="pop-up-item">
          <div class="name">Ссылка на продукт</div>
          <input type="text" v-model="banner.targetProductPath">
        </div>
        <button type="submit">Создать</button>
      </form>
    </div>

    <button class="create" @click="showModal = true">Создать</button>

    <table v-if="banners">
      <thead>
      <tr>
        <th>ID</th>
        <th>Заголовок</th>
        <th>Картинка</th>
        <th>Текст</th>
        <th>Путь</th>
        <th>Удаление</th>
      </tr>
      </thead>
      <tbody>
      <tr v-for="item in banners">
        <td>{{ item.id }}</td>
        <td>{{ item.title }}</td>
        <td>
          <img :src="'http://' + item.imagePath">
        </td>
        <td>{{ item.text }}</td>
        <td>{{ item.targetProductPath }}</td>
        <td>
          <button class="delete" @click="deleteBanner(item.id)">
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
  padding: 20px 90px;
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


td img {
  width: 50px;
  height: 50px;
}

button img {
  width: 30px;
  height: 30px;
}


.pop-up {
  background-color: #fff;
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
  border-radius: 8px;
  overflow: hidden;
  width: 310px;
  position: fixed;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  padding: 20px;
}

.close {
  width: 20px;
  height: 20px;
  background-color: #3bb07c;
  cursor: pointer;
  display: flex;
  justify-content: center;
  align-items: center;
  position: absolute;
  top: 10px;
  right: 10px;
}

.close img {
  width: 15px;
  height: 15px;
  display: block;
}

.title-create {
  background-color: #3bb07c;
  color: #fff;
  padding: 10px;
  font-weight: bold;
  text-align: center;
  margin-bottom: 20px;
}

.pop-up-item {
  margin-bottom: 15px;
}

.name {
  font-weight: bold;
  margin-bottom: 5px;
}

.pop-up input[type="text"],
.pop-up textarea,
.pop-up input[type="file"] {
  width: 100%;
  padding: 8px;
  box-sizing: border-box;
  margin-top: 5px;
  margin-bottom: 10px;
  border: 1px solid #ccc;
  border-radius: 4px;
  font-size: 14px;
}

.pop-up button {
  background-color: #3bb07c;
  color: #fff;
  padding: 10px;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  font-size: 16px;
  width: 100%;
}

.pop-up button:hover {
  background-color: #3bb07c;
}

.close {
  background-color: #fe696a;
}
</style>