import { createRouter, createWebHashHistory } from 'vue-router'
import TurnosList from '../views/TurnosList.vue'
import TurnoNuevo from '../views/TurnoNuevo.vue'
import TurnoDetalle from '../views/TurnoDetalle.vue'
import PacientesList from '../views/PacientesList.vue'

const routes = [
  { path: '/',              redirect: '/turnos'       },
  { path: '/turnos',        component: TurnosList     },
  { path: '/turnos/nuevo',  component: TurnoNuevo     },
  { path: '/turnos/:id',    component: TurnoDetalle   },
  { path: '/pacientes',     component: PacientesList  }
]

const router = createRouter({
  history: createWebHashHistory(),
  routes
})

export default router
