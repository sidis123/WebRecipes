import { legacy_createStore as createStore } from 'redux'

const initialState = {
  sidebarShow: true,
  user: {},
  theme: 'light',
}

const changeState = (state = initialState, { type, ...rest }) => {
  switch (type) {
    case 'set':
      return { ...state, ...rest }
    case 'set_user':
      return { ...state, user: rest.user }
    default:
      return state
  }
}

const store = createStore(changeState)
export default store
