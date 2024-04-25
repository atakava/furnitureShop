import {defineStore} from 'pinia'

export const useStore = defineStore('product', {
    state: () => {
        return {
            user: {
                password: "",
                name: "",
            }
        }
    }
});