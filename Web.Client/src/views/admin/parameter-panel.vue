<script>
import axios from 'axios';

export default {
  name: "parameter-panel",
  data() {
    return {
      parameters: null,
      name: "",
      value: "",
      showModal: false
    }
  },
  methods: {
    async fetchParameter() {
      try {
        const response = await axios.post(
            "http://localhost:5199/api/Parameter/GetAll",
            null,
            {
              withCredentials: true,
              headers: {
                'Content-Type': 'application/json'
              }
            });

        this.parameters = response.data.data;

      } catch (e) {
        console.error("ошибка получени параметров", e);
        throw e;
      }
    },
    async deleteParameter(id) {
      try {
        const response = await axios.post(
            "http://localhost:5199/api/Parameter/Delete",
            {
              id: id
            },
            {
              withCredentials: true,
              headers: {
                'Content-Type': 'application/json'
              }
            });

        await this.fetchParameter();

      } catch (e) {
        console.error("ошибка удлаения парамета", e);
        throw e;
      }
    },

    async createParameter() {
      try {
        const response = await axios.post(
            "http://localhost:5199/api/Parameter/Create",
            {
              name: this.name,
              value: this.value
            },
            {
              withCredentials: true,
              headers: {
                'Content-Type': 'application/json'
              }
            });

        await this.fetchParameter();
        this.showModal = false;

        this.name = "";
        this.value = "";
        
      } catch (e) {
        console.error("ошибка создания парамета", e);
        throw e;
      }
    }

  },
  async created() {
    await this.fetchParameter();
  }
}
</script>

<template>
  <div class="container">

    <form class="popup" @submit.prevent="createParameter" v-if="showModal">
      <div class="close" @click="showModal = false">
        <img src="/icons/clear.svg" alt="close">
      </div>
      <div class="title">Создание параметра</div>
      <div class="item">
        <div class="name">Название</div>
        <input v-model="name">
      </div>
      <div class="item">
        <div class="name">Значение</div>
        <input v-model="value">
      </div>
      <button type="submit">Создать</button>
    </form>
    
    <button class="create" @click="showModal = true">Создать</button>
    
    <table v-if="parameters">
      <thead>
      <tr>
        <th>ID</th>
        <th>Название</th>
        <th>Значение</th>
        <th>Удаление</th>
      </tr>
      </thead>
      <tbody>
      <tr v-for="item in parameters">
        <td>{{ item.id }}</td>
        <td>{{ item.name }}</td>
        <td>{{ item.value }}</td>
        <td>
          <button class="delete" @click="deleteParameter(item.id)">
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

.popup {
  background-color: #fff;
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
  padding: 20px;
  border-radius: 8px;
  width: 300px;
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
}

.title {
  font-size: 20px;
  font-weight: bold;
  margin-bottom: 15px;
}

.item {
  margin-bottom: 15px;
}

.name {
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

.popup button {
  background-color: #4CAF50;
  color: white;
  padding: 10px 15px;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  font-size: 16px;
}

.popup button:hover {
  background-color: #45a049;
}

</style>