import { ReactNode } from 'react'
import './globals.css'
import {
  Roboto_Flex as Roboto,
  Bai_Jamjuree as BaiJamjuree,
} from 'next/font/google'
import { DefaultSeo } from 'next-seo'
import SEO from '../../next-seo.config'
import Head from 'next/head'

const roboto = Roboto({ subsets: ['latin'], variable: '--font-roboto' })

const baiJamjuree = BaiJamjuree({
  subsets: ['latin'],
  weight: '700',
  variable: '--font-bai-jamjuree',
})

export const metadata = {
  title: 'Tech One',
  description: 'Crud de usuarios feito como teste para vaga de estagiario',
}

export default function RootLayout({ children }: { children: ReactNode }) {
  return (
    <html lang="pt-br">
      <Head>
        {/* Configurações globais de SEO */}
        <DefaultSeo {...SEO} />
      </Head>

      <body
        className={`${roboto.variable} ${baiJamjuree.variable} bg-[#312E38] text-[#F4EDE8]`}
      >
        {children}
      </body>
    </html>
  )
}
