import { User } from 'lucide-react'

interface CardsProps {
  name: string
  details: () => void
}

export function Cards({ name, details }: CardsProps) {
  return (
    <button className="mb-4 flex w-full flex-col gap-7" onClick={details}>
      <div className="w-full rounded-[10px] bg-[#3E3B47] p-4 hover:bg-[#4b4855]">
        <div className="flex items-center justify-between">
          <h2>{name}</h2>
          <User />
        </div>
      </div>
    </button>
  )
}
