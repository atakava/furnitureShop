<script>
import axios from "axios";

export default {
  name: "catalog-panel",
  data() {
    return {
      catalog: null,
      title: "",
      showModal: false
    };
  },
  methods: {
    async createElement() {
      try {
        const response = await axios.post(
            "http://localhost:5199/api/Catalog/Create",
            {title: this.title},
            {
              withCredentials: true,
              headers: {
                'Content-Type': 'application/json'
              }
            }
        );

        console.log('Create catalog', response);
        this.showModal = false;
        this.title = "";
        await this.fetchCatalog();

      } catch (e) {
        console.error("ошибка создание каталога", e);
        throw e;
      }
    },
    async deleteElement(id) {
      try {
        const response = await axios.post(
            "http://localhost:5199/api/Catalog/Delete",
            {id: id},
            {
              withCredentials: true,
              headers: {
                'Content-Type': 'application/json'
              }
            }
        );

        console.log('Delete catalog', response);

        this.catalog = response.data.value;

      } catch (e) {
        console.error("ошибка удаления каталога", e);
        throw e;
      }
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
    }
  },
 

  async mounted() {
    await this.fetchCatalog();
  }
}
</script>

<template>
  <div class="container">

    <div class="pop-up" v-if="showModal">
      <form @submit.prevent="createElement">
        <div class="close" @click="showModal = false">
          <img src="/icons/clear.svg" alt="close">
        </div>
        <div class="title-create">Создание каталога</div>
        <div class="pop-up-item">
          <div class="name">Название:</div>
          <input type="text" v-model="title">
        </div>
        <button type="submit">Создать</button>
      </form>
    </div>

    <button class="create" @click="showModal = true">Создать</button>

    <table v-if="catalog">
      <thead>
      <tr>
        <th>ID</th>
        <th>Название</th>
        <th>Удаление</th>
      </tr>
      </thead>
      <tbody>
      <tr v-for="item in catalog">
        <td>{{ item.id }}</td>
        <td>{{ item.title }}</td>
        <td>
          <button class="delete" @click="deleteElement(item.id)">
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

.delete {
  width: 30px;
  height: 30px;
  background-color: #fe696a;
  display: flex;
  align-items: center;
  justify-content: center;
}

.delete img {
  width: 30px;
  height: 30px;
}

.pop-up {
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  border: 1px solid #000;
  padding: 35px;
  border-radius: 4px;
  width: 540px;
  height: 150px;
}

.pop-up .title-create {
  font-size: 24px;
  text-align: center;
  margin-bottom: 30px;
}

.pop-up-item {
  display: flex;
}

.name {
  font-size: 18px;
  font-weight: 500;
}

.pop-up button {
  background-color: #3bb07c;
  color: #fff;
  padding: 10px 20px;
  position: absolute;
  bottom: 10%;
  right: 5%;
}

.pop-up input {
  border: 1px solid rgba(0, 0, 0, 50%);
  margin-left: 10px;
  outline: none;
  padding: 0 10px;
}
</style>