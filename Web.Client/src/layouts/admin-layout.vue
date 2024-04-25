<script>
import NavbarElement from "@/components/admin/navbar-element.vue";
import axios from "axios";

export default {
  name: "admin-layout",
  components: {NavbarElement},
  data() {
    return {
      user: {
        id: 1,
        password: "",
        name: "",
      },
      success: false
    };
  },
  methods: {
    async logIn() {
      try {
        const response = await axios.post(
            "http://localhost:5199/api/User/SingIn",
            this.user,
            {
              withCredentials: true,
              headers: {
                'Content-Type': 'application/json'
              }
            }
        );

        if (response.data.success === true) {
          localStorage.setItem("admin", JSON.stringify(this.user));
          this.success = true;
        }

      } catch (e) {
        console.error("ошибка входа в админ панель", e);
        throw e;
      }
    }
  },
};
</script>

<template>
  <div class="container" v-if="success">
    <navbar-element/>
    <main>
      <slot></slot>
    </main>
  </div>
  <form @submit.prevent="logIn" class="auth" v-else>
    <div class="modal-content">
      <h2>Вход в админ-панель</h2>
      <div class="form-group">
        <label for="username">Имя пользователя:</label>
        <input type="text" v-model="user.name" required/>
      </div>
      <div class="form-group">
        <label for="password">Пароль:</label>
        <input type="password" v-model="user.password" required/>
      </div>
      <button type="submit">Войти</button>
    </div>
  </form>
</template>

<style scoped>
.container {
  display: flex;
  width: 100%;
}

.modal {
  display: none;
  position: fixed;
  z-index: 1;
  left: 0;
  top: 0;
  width: 100%;
  height: 100%;
  overflow: auto;
  background-color: rgba(0, 0, 0, 0.4);
}

.modal-content {
  background-color: #fefefe;
  margin: 15% auto;
  padding: 20px;
  border: 1px solid #888;
  width: 80%;
}

.close {
  color: #aaa;
  float: right;
  font-size: 28px;
  font-weight: bold;
  cursor: pointer;
}

.close:hover,
.close:focus {
  color: black;
  text-decoration: none;
  cursor: pointer;
}

h2 {
  text-align: center;
}

form {
  display: flex;
  flex-direction: column;
  align-items: center;
}

.form-group {
  margin-bottom: 15px;
}

label {
  display: block;
  font-weight: bold;
}

input {
  width: 100%;
  padding: 8px;
  box-sizing: border-box;
  border: 1px solid #ccc;
  border-radius: 4px;
  font-size: 16px;
  margin-bottom: 15px;
}

button {
  background-color: #fe696a;
  color: white;
  padding: 10px 15px;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  font-size: 16px;
}

button:hover {
  background-color: #d45c5d;
}
</style>