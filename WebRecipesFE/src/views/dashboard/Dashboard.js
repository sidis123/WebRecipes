import React, { useEffect, useState } from 'react'
import classNames from 'classnames'
import axios from 'axios'

import {
  CAvatar,
  CButton,
  CButtonGroup,
  CCard,
  CCardBody,
  CCardFooter,
  CCardHeader,
  CCol,
  CProgress,
  CRow,
  CTable,
  CTableBody,
  CTableDataCell,
  CTableHead,
  CTableHeaderCell,
  CTableRow,
} from '@coreui/react'
import CIcon from '@coreui/icons-react'
import {
  cibCcAmex,
  cibCcApplePay,
  cibCcMastercard,
  cibCcPaypal,
  cibCcStripe,
  cibCcVisa,
  cibGoogle,
  cibFacebook,
  cibLinkedin,
  cifBr,
  cifEs,
  cifFr,
  cifIn,
  cifPl,
  cifUs,
  cibTwitter,
  cilCloudDownload,
  cilPeople,
  cilUser,
  cilUserFemale,
} from '@coreui/icons'

import avatar1 from 'src/assets/images/avatars/1.jpg'
import avatar2 from 'src/assets/images/avatars/2.jpg'
import avatar3 from 'src/assets/images/avatars/3.jpg'
import avatar4 from 'src/assets/images/avatars/4.jpg'
import avatar5 from 'src/assets/images/avatars/5.jpg'
import avatar6 from 'src/assets/images/avatars/6.jpg'

import WidgetsBrand from '../widgets/WidgetsBrand'
import WidgetsDropdown from '../widgets/WidgetsDropdown'
import MainChart from './MainChart'
import { useSelector } from 'react-redux'

const Dashboard = () => {
  const [recipes, setRecipes] = useState([])
  const user = useSelector((state) => state.user)
  const [recipiesLoading, setRecipiesLoading] = useState(true)
  useEffect(() => {
    console.log('User:', user)
    fetchRecipes()
    if (!recipiesLoading) {
      console.log('Recipes:', recipes)
    }
  }, [])

  const fetchRecipes = async () => {
    setRecipiesLoading(true)
    axios
      .get('https://localhost:7120/api/Recipe')
      .then((response) => {
        setRecipes(response.data)
        console.log('Recipes:', response.data)
        setRecipiesLoading(false)
      })
      .catch((error) => {
        setRecipiesLoading(false)
        console.error('Error fetching recipes:', error)
      })
  }

  if (recipiesLoading) {
    return (
      <div className="d-flex justify-content-center align-items-center">
        <CProgress color="info" />
      </div>
    )
  } else {
    return (
      <>
        <h1>home</h1>
        {recipes.map((r, index) => (
          <div key={index}>
            <h2>{r.pavadinimas}</h2>
            <img src={`${r.pictureUrl}`} alt={r.pavadinimas} />
          </div>
        ))}
      </>
    )
  }
}

export default Dashboard
