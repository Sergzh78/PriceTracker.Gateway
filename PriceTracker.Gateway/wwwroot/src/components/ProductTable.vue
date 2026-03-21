<template>
  <div class="container my-4">
    <div class="table-responsive">
      <table class="table table-striped table-hover">
        <thead>
          <tr>
            <th>Название</th>
            <th class="d-none d-md-table-cell">Цена</th>
            <th class="d-none d-lg-table-cell">Дата</th>
            <th>Действие</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="item in products" :key="item.id" @click="viewDetails(item)" style="cursor: pointer">
            <td>{{ item.name }}</td>
            <td class="d-none d-md-table-cell">{{ item.price }} ₽</td>
            <td class="d-none d-lg-table-cell">{{ item.date }}</td>
            <td>
              <button class="btn btn-sm btn-primary" @click.stop="viewDetails(item)">
                График
              </button>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue'

const products = ref([
  { id: 1, name: 'Тапки', price: 1000, date: '21.03.2026' },
  { id: 2, name: 'Кеды', price: 2000, date: '21.03.2026' },
  { id: 3, name: 'Кроссовки', price: 3000, date: '21.03.2026' },
])

const viewDetails = (item) => {  
  // TODO: график
}
</script>

<style scoped>
.table-hover tbody tr:hover {
  background-color: rgba(0, 0, 0, 0.075);
}
</style>
