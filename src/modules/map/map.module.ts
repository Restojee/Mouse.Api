import { Module } from '@nestjs/common';
import { MapService } from './map.service';
import { MapController } from './map.controller';
import { MapRepository } from './repositories/map.repository';
import { DatabaseModule } from "../../database/database.module";
import { MapEntity } from "../../database/entities/map.entity";
import { CompletedRepository } from "./repositories/completed.repository";
import { FavoriteEntity } from "../../database/entities/favorite.entity";
import { CompletedEntity } from "../../database/entities/completed.entity";
import {FavoriteRepository} from "./repositories/favorite.repository";


@Module({
  imports: [DatabaseModule],
  providers: [
    MapService,
    MapRepository,
    FavoriteRepository,
    CompletedRepository,
    {
      provide: 'MAPS_REPOSITORY',
      useValue: MapEntity,
    },
    {
      provide: 'FAVORITE_REPOSITORY',
      useValue: FavoriteEntity,
    },
    {
      provide: 'COMPLETED_REPOSITORY',
      useValue: CompletedEntity,
    }
  ],
  controllers: [MapController],
  exports: [ MapService ]
})
export class MapModule {}
