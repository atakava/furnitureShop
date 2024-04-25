<script>
import axios from 'axios';

export default {
  name: "user-panel",
  data() {
    return {
      users: null
    }
  },
  methods: {
    async fetchUsers() {
      try {
        const response = await axios.post(
            "http://localhost:5199/api/User/GetAll",
            null,
            {
              withCredentials: true,
              headers: {
                'Content-Type': 'application/json'
              }
            });

        this.users = response.data;

      } catch (e) {
        console.error("ошибка получени параметров", e);
        throw e;
      }
    },
    async deleteUser(id) {
      try {
        const response = await axios.post(
            "http://localhost:5199/api/User/Delete",
            {
              id: id
            },
            {
              withCredentials: true,
              headers: {
                'Content-Type': 'application/json'
              }
            });

        await this.fetchUsers();

      } catch (e) {
        console.error("ошибка удлаения пользователя", e);
        throw e;
      }
    }
  },
  async created() {
    await this.fetchUsers();
  }
}
</script>

<template>
  <div class="container" v-if="users">
    <div class="title">
      Пользователи
    </div>

    <table>
      <thead>
      <tr>
        <th>ID</th>
        <th>Имя</th>
        <th>Номер</th>
        <th>Удаление</th>
      </tr>
      </thead>
      <tbody>
      <tr v-for="item in users">
        <td>{{ item.id }}</td>
        <td>{{ item.name }}</td>
        <td>{{ item.phone }}</td>
        <td>
          <button class="delete" @click="deleteUser(item.id)">
            <img src="/icons/clear.svg" alt="clear">
          </button>
        </td>
      </tr>
      </tbody>
    </table>
  </div>
  <div class="container" v-else>
    <p class="title">еще ни 1 пользователь не зарегестрировался</p>
  </div>
</template>

<style scoped>

.container {
  padding: 0 90px;
  width: 1300px;
}

.title {
  font-size: 28px;
  font-weight: 600;
  margin: 20px 0 35px 0;
}

</style>