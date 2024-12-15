import React from 'react'
import { CFooter } from '@coreui/react'

const AppFooter = () => {
  const googleFont = document.createElement('link')
  googleFont.href = 'https://fonts.googleapis.com/css2?family=DynaPuff:wght@400..700&display=swap'
  googleFont.rel = 'stylesheet'
  document.head.appendChild(googleFont)

  const footerStyle = {
    fontFamily: '"DynaPuff", cursive',
    fontSize: '14px',
    color: '#ffffff',
    display: 'flex',
    flexDirection: 'column',
    justifyContent: 'center',
    alignItems: 'center',
    textAlign: 'center',
    padding: '10px 15px',
  }

  const boldTextStyle = {
    fontFamily: '"Roboto", sans-serif',
    fontWeight: 700,
    color: '#bbb',
    marginTop: '5px',
  }

  const containerStyle = {
    display: 'flex',
    justifyContent: 'space-between',
    alignItems: 'center',
    width: '100%',
  }

  return (
    <CFooter className="px-4" style={footerStyle}>
      <div style={containerStyle}>
        <span>
          Saityno taikomųjų programų projektavimo modulio projektas &quot;WebRecipes&quot;
        </span>
        <span style={boldTextStyle}>Tomas Sakalauskas IFF-1/4</span>
      </div>
    </CFooter>
  )
}

export default React.memo(AppFooter)
