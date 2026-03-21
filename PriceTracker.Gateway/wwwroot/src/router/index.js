import { createRouter, createWebHistory } from 'vue-router'

// 
import HomePage from '../components/HomePage.vue'
// import AboutPage from '../components/AboutPage.vue'
// import ContactsPage from '../components/ContactsPage.vue'

const routes = [
  {
    path: '/',
    name: 'home',
    component: HomePage
  },  
  // {
  //   path: '/about',
  //   name: 'about',
  //   component: AboutPage
  // },
  // {
  //   path: '/contacts',
  //   name: 'contacts',
  //   component: ContactsPage
  // }
]

const router = createRouter({
  history: createWebHistory(),  //
  routes
})

export default router